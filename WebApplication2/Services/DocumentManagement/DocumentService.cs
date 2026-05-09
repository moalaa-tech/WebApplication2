using AutoMapper;
using CRM.Domain.Entities.DocumentManagement;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.DocumentManagement
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IRepository<DocumentShare> _DocumentShareRepository;

        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public DocumentService(
            IRepository<Document> documentRepository,
            IWebHostEnvironment environment,
            IRepository<DocumentShare> DocumentShareRepository,
            IMapper mapper)
        {
            _documentRepository = documentRepository;
            _environment = environment;
            _mapper = mapper;
            _DocumentShareRepository = DocumentShareRepository;
        }


        public async Task<DocumentDto> GetByIdAsync(int id)
        {
            var document = await _documentRepository.GetAll()
                .Include(d => d.Category)
                .Include(d => d.Folder)
                .FirstOrDefaultAsync(d => d.Id == id);

            return _mapper.Map<DocumentDto>(document);
        }


        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _documentRepository.GetAll()
                .Include(d => d.Category)
                .Include(d => d.Folder)
                .Where(d => d.IsLatestVersion && d.Status == "Active")
                .ToListAsync();

            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> CreateAsync(CreateDocumentDto createDto, string uploadedBy)
        {
            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "documents");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var file = createDto.File;
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var document = new Document
            {
                Title = createDto.Title,
                Description = createDto.Description,
                DocumentType = createDto.DocumentType,
                FileName = file.FileName,
                FileSize = file.Length,
                FileExtension = Path.GetExtension(file.FileName).TrimStart('.'),
                FilePath = filePath,
                MimeType = file.ContentType,
                CategoryId = createDto.CategoryId,
                FolderId = createDto.FolderId,
                UploadedBy = uploadedBy,
                ExpiryDate = createDto.ExpiryDate,
                IsPublic = createDto.IsPublic,
                Tags = createDto.Tags,
                Status = "Active"
            };

            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();

            return _mapper.Map<DocumentDto>(document);
        }

        public async Task UpdateAsync(int id, UpdateDocumentDto updateDto, string modifiedBy)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
                throw new KeyNotFoundException($"Document with ID {id} not found.");

            // Handle file update if provided
            if (updateDto.File != null)
            {
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "documents");
                var uniqueFileName = $"{Guid.NewGuid()}_{updateDto.File.FileName}";
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateDto.File.CopyToAsync(stream);
                }

                // Update file properties
                document.FileName = updateDto.File.FileName;
                document.FileSize = updateDto.File.Length;
                document.FileExtension = Path.GetExtension(updateDto.File.FileName).TrimStart('.');
                document.FilePath = filePath;
                document.MimeType = updateDto.File.ContentType;
                document.Version++;
            }

            // Update other properties
            document.Title = updateDto.Title;
            document.Description = updateDto.Description;
            document.DocumentType = updateDto.DocumentType;
            document.CategoryId = updateDto.CategoryId;
            document.FolderId = updateDto.FolderId;
            document.ExpiryDate = updateDto.ExpiryDate;
            document.IsPublic = updateDto.IsPublic;
            document.Status = updateDto.Status;
            document.Tags = updateDto.Tags;
            document.ModifiedDate = DateTime.UtcNow;

            _documentRepository.Update(document);
            await _documentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
                throw new KeyNotFoundException($"Document with ID {id} not found.");

            // Delete physical file
            if (File.Exists(document.FilePath))
                File.Delete(document.FilePath);

            _documentRepository.Remove(document);
            await _documentRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _documentRepository.ExistsAsync(d => d.Id == id);

        public async Task IncrementDownloadCountAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document != null)
            {
                document.DownloadCount++;
                _documentRepository.Update(document);
                await _documentRepository.SaveChangesAsync();
            }
        }

        public async Task<Stream> GetFileStreamAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null || !File.Exists(document.FilePath))
                throw new FileNotFoundException("Document not found");

            return new FileStream(document.FilePath, FileMode.Open, FileAccess.Read);
        }

        public async Task<IEnumerable<DocumentDto>> GetByCategoryAsync(int categoryId)
        {
            var documents = _documentRepository.GetByCondition(d => d.CategoryId == categoryId && d.IsLatestVersion && d.Status == "Active");
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<IEnumerable<DocumentDto>> SearchAsync(string searchTerm)
        {
            var documents = _documentRepository.GetByCondition(d =>
                (d.Title.Contains(searchTerm) || d.Description.Contains(searchTerm) || d.Tags.Contains(searchTerm)) &&
                d.IsLatestVersion && d.Status == "Active");
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<IEnumerable<DocumentDto>> GetByFolderAsync(int folderId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ShareDocumentAsync(ShareDocumentRequestDto shareDto, int sharedByUserId)
        {
            var existingShare = await _DocumentShareRepository.GetAll()
                .FirstOrDefaultAsync(ds => ds.DocumentId == shareDto.DocumentId &&
                                         ds.SharedWithUserId == shareDto.SharedWithUserId);

            if (existingShare != null)
            {
                // Update existing share
                existingShare.PermissionLevel = shareDto.PermissionLevel;
                existingShare.ExpiryDate = shareDto.ExpiryDate;
                existingShare.CanDownload = shareDto.CanDownload;

                _DocumentShareRepository.Update(existingShare);
            }
            else
            {
                // Create new share
                var documentShare = new DocumentShare
                {
                    DocumentId = shareDto.DocumentId,
                    SharedWithUserId = shareDto.SharedWithUserId,
                    SharedByUserId = sharedByUserId,
                    PermissionLevel = shareDto.PermissionLevel,
                    ExpiryDate = shareDto.ExpiryDate,
                    CanDownload = shareDto.CanDownload
                };

                await _DocumentShareRepository.AddAsync(documentShare);
            }
            await _DocumentShareRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DocumentShareDto>> GetDocumentSharesAsync(int documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveShareAsync(int shareId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateDownloadUrlAsync(int documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IncrementViewCountAsync(int documentId)
        {
            throw new NotImplementedException();
        }
    }
}
