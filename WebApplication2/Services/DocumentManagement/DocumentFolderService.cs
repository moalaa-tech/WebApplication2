using AutoMapper;
using CRM.Domain.Entities.DocumentManagement;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.DocumentManagement
{
    public class DocumentFolderService : IDocumentFolderService
    {
        private readonly IRepository<DocumentFolder> _folderRepository;
        private readonly IRepository<Document> DocumentRepository;

        private readonly IMapper _mapper;

        public DocumentFolderService(
            IRepository<DocumentFolder> folderRepository,
            IRepository<Document> DocumentRepository,
            IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
            this.DocumentRepository = DocumentRepository;
        }


        public async Task<IEnumerable<DocumentFolderDto>> GetAllFoldersAsync()
        {
            var folders = await _folderRepository.GetAll()
                .Include(f => f.ParentFolder)
                .Include(f => f.Documents)
                .Include(f => f.SubFolders)
                .OrderBy(f => f.Path)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DocumentFolderDto>>(folders);
        }

        public async Task<DocumentFolderDto> GetFolderByIdAsync(int id)
        {
            var folder = await _folderRepository.GetAll()
                .Include(f => f.ParentFolder)
                .Include(f => f.Documents)
                .Include(f => f.SubFolders)
                .ThenInclude(sf => sf.Documents)
                .FirstOrDefaultAsync(f => f.Id == id);

            return _mapper.Map<DocumentFolderDto>(folder);
        }


        public async Task<DocumentFolderDto> CreateFolderAsync(CreateDocumentFolderDto createDto)
        {
            // Check if folder with same name already exists in the same parent
            var existingFolder = await _folderRepository.GetAll()
                .FirstOrDefaultAsync(f => f.Name == createDto.Name &&
                                        f.ParentFolderId == createDto.ParentFolderId);

            if (existingFolder != null)
            {
                throw new InvalidOperationException("A folder with this name already exists in the selected location.");
            }

            var folder = _mapper.Map<DocumentFolder>(createDto);
            folder.Path = await GenerateFolderPathAsync(folder);
            folder.IsActive = true;
            await _folderRepository.AddAsync(folder);
            await _folderRepository.SaveChangesAsync();

            return _mapper.Map<DocumentFolderDto>(folder);
        }


        public async Task<DocumentFolderDto> UpdateFolderAsync(UpdateDocumentFolderDto updateDto)
        {
            var folder = await _folderRepository.GetByIdAsync(updateDto.Id);
            if (folder == null)
            {
                throw new KeyNotFoundException("Folder not found.");
            }

            // Check for name conflict if name changed
            if (folder.Name != updateDto.Name)
            {
                var existingFolder = await _folderRepository.GetAll()
                    .FirstOrDefaultAsync(f => f.Name == updateDto.Name &&
                                            f.ParentFolderId == updateDto.ParentFolderId &&
                                            f.Id != updateDto.Id);

                if (existingFolder != null)
                {
                    throw new InvalidOperationException("A folder with this name already exists in the selected location.");
                }
            }

            _mapper.Map(updateDto, folder);
            folder.DateModified = DateTime.UtcNow;

            // Regenerate path if parent changed
            if (folder.ParentFolderId != updateDto.ParentFolderId)
            {
                folder.Path = await GenerateFolderPathAsync(folder);
            }

            _folderRepository.Update(folder);
            await _folderRepository.SaveChangesAsync();

            return _mapper.Map<DocumentFolderDto>(folder);
        }


        public async Task<bool> DeleteFolderAsync(int id)
        {
            var folder = await _folderRepository.GetByIdAsync(id);
            if (folder == null)
            {
                return false;
            }

            if (!await CanDeleteFolderAsync(id))
            {
                throw new InvalidOperationException("Cannot delete folder. It contains documents or subfolders.");
            }

            _folderRepository.Delete(folder);
            await _folderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CanDeleteFolderAsync(int folderId)
        {
            var hasDocuments = await  DocumentRepository.GetAll().AnyAsync(d => d.FolderId == folderId);
            var hasSubFolders = await _folderRepository.GetAll().AnyAsync(f => f.ParentFolderId == folderId);

            return !hasDocuments && !hasSubFolders;
        }

        public async Task<string> GenerateFolderPathAsync(DocumentFolder folder)
        {
            if (folder.ParentFolderId == null)
            {
                return $"/{folder.Name}";
            }

            var parentFolder = await _folderRepository.GetAll()
                .FirstOrDefaultAsync(f => f.Id == folder.ParentFolderId);

            return $"{parentFolder?.Path}/{folder.Name}";
        }


        public async Task<IEnumerable<FolderTreeDto>> GetFolderTreeAsync()
        {
            var folders = await _folderRepository.GetAll()
                .Include(f => f.Documents)
                .Include(f => f.SubFolders)
                .Where(f => f.ParentFolderId == null)
                .OrderBy(f => f.Name)
                .ToListAsync();

            return BuildFolderTree(folders);
        }

        private List<FolderTreeDto> BuildFolderTree(List<DocumentFolder> folders)
        {
            var tree = new List<FolderTreeDto>();

            foreach (var folder in folders)
            {
                var node = new FolderTreeDto
                {
                    Id = folder.Id,
                    Name = folder.Name,
                    Path = folder.Path,
                    ParentFolderId = folder.ParentFolderId,
                    DocumentCount = folder.Documents?.Count ?? 0,
                    Children = folder.SubFolders != null ? BuildFolderTree(folder.SubFolders.ToList()) : new List<FolderTreeDto>()
                };
                tree.Add(node);
            }

            return tree;
        }

        public async Task<int> GetFolderDocumentCountAsync(int folderId)
        {
            return await DocumentRepository.GetAll().CountAsync(d => d.FolderId == folderId);
        }

        public async Task<int> GetFolderSubFolderCountAsync(int folderId)
        {
            return await _folderRepository.GetAll().CountAsync(f => f.ParentFolderId == folderId);
        }

        public async Task<bool> MoveFolderAsync(int folderId, int? newParentFolderId)
        {
            var folder = await _folderRepository.GetByIdAsync(folderId);
            if (folder == null)
            {
                return false;
            }

            // Check if moving to a subfolder of itself
            if (newParentFolderId.HasValue && await IsDescendant(folderId, newParentFolderId.Value))
            {
                throw new InvalidOperationException("Cannot move folder to one of its own subfolders.");
            }

            folder.ParentFolderId = newParentFolderId;
            folder.Path = await GenerateFolderPathAsync(folder);
            folder.DateModified = DateTime.UtcNow;

            _folderRepository.Update(folder);
            await _folderRepository.SaveChangesAsync();
            return true;
        }


        private async Task<bool> IsDescendant(int potentialParentId, int folderId)
        {
            var currentFolderId = folderId;
            while (currentFolderId != null)
            {
                var folder = await _folderRepository.GetAll()
                    .FirstOrDefaultAsync(f => f.Id == currentFolderId);

                if (folder == null)
                {
                    return false;
                }

                if (folder.ParentFolderId == potentialParentId)
                {
                    return true;
                }

                currentFolderId = folder.ParentFolderId.Value;
            }

            return false;
        }

        public Task<string> GenerateFolderPathAsync(int folderId)
        {
            throw new NotImplementedException();
        }
    }
}
