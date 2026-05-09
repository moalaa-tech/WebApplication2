using CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public interface IKnowledgeBaseService
    {
        Task<IEnumerable<KnowledgeBaseArticleDto>> GetAllArticlesAsync();
        Task<KnowledgeBaseArticleDto> GetArticleByIdAsync(int id);
        Task<int> CreateArticleAsync(KnowledgeBaseArticleCreateDto articleDto);
        Task UpdateArticleAsync(KnowledgeBaseArticleUpdateDto articleDto);
        Task DeleteArticleAsync(int id);
        Task<IEnumerable<KnowledgeBaseArticleDto>> GetArticlesByCategoryAsync(string category);
        Task<IEnumerable<KnowledgeBaseArticleDto>> SearchArticlesAsync(string searchTerm);
    }
}
