using AutoMapper;
using CRM.Domain.Entities.DocumentManagement;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.DocumentManagement
{
    public class DocumentCategoryService : IDocumentCategoryService
    {
        private readonly IRepository<DocumentCategory> _repository;
        private readonly IRepository<Document> _DocumentRepository;

        private readonly IMapper _mapper;

        public DocumentCategoryService(
            IRepository<DocumentCategory> repository, 
            IMapper mapper,
            IRepository<Document> DocumentRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _DocumentRepository = DocumentRepository;
        }

        public async Task<IEnumerable<DocumentCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAll()
                .Include(c => c.ParentCategory)
                .Include(c => c.Documents)
                .OrderBy(c => c.Order)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DocumentCategoryDto>>(categories);
        }

        public async Task<DocumentCategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetAll()
                .Include(c => c.ParentCategory)
                .Include(c => c.Documents)
                .OrderBy(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<DocumentCategoryDto>(category);
        }

        public async Task<DocumentCategoryDto> CreateCategoryAsync(CreateDocumentCategoryDto createDto)
        {
            var category = _mapper.Map<DocumentCategory>(createDto);
            category.Slug = GenerateSlug(createDto.Name);

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return _mapper.Map<DocumentCategoryDto>(category);
        }

        public async Task<DocumentCategoryDto> UpdateCategoryAsync(int id, UpdateDocumentCategoryDto updateDto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category not found");

            _mapper.Map(updateDto, category);
            category.Slug = GenerateSlug(updateDto.Name);

            _repository.Update(category);
            await _repository.SaveChangesAsync();

            return _mapper.Map<DocumentCategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return false;

            // Check if category has documents or children
            var hasDocuments = await _DocumentRepository.ExistsAsync(d => d.CategoryId == id);
            var hasChildren = await _repository.ExistsAsync(c => c.ParentCategoryId == id);

            if (hasDocuments || hasChildren)
                return false;

            _repository.Delete(category);
             await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DocumentCategoryDto>> GetActiveCategoriesAsync()
        {
            var categories = await _repository.GetAll()
                .Include(c => c.ParentCategory)
                .Where(c => c.IsActive)
                .OrderBy(c => c.Order)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DocumentCategoryDto>>(categories);
        }

        public async Task<IEnumerable<DocumentCategoryDto>> GetCategoriesWithDocumentsAsync()
        {
            var categories = await _repository.GetAll()
                .Include(c => c.Documents)
                .Where(c => c.IsActive)
                .OrderBy(c => c.Order)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DocumentCategoryDto>>(categories);
        }

        private string GenerateSlug(string name)
        {
            return name.ToLower().Replace(" ", "-");
        }

    }
}
