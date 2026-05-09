using AutoMapper;
using CRM.Domain.Entities.CustomerService;
using CRM.WebApp.DTOs.CustomerService.KBArticle;
using CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public class KnowledgeBaseService : IKnowledgeBaseService
    {
        private readonly IRepository<KnowledgeBaseArticle> _articleRepository;
        private readonly IRepository<KBArticleAttachment> _attachmentRepository;
        private readonly IRepository<KBArticleComment> _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<KnowledgeBaseService> _logger;

        public KnowledgeBaseService(
            IRepository<KnowledgeBaseArticle> articleRepository,
            IRepository<KBArticleAttachment> attachmentRepository,
            IRepository<KBArticleComment> commentRepository,
            IMapper mapper,
            ILogger<KnowledgeBaseService> logger)
        {
            _articleRepository = articleRepository;
            _attachmentRepository = attachmentRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<KnowledgeBaseArticleDto>> GetPublishedArticlesAsync()
        {
            try
            {
                var articles = await _articleRepository.GetAllAsync(a => a.Author).Where(a => a.IsPublished)
                    .OrderByDescending(a => a.CreatedDate).ToListAsync();

                return _mapper.Map<IEnumerable<KnowledgeBaseArticleDto>>(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving published articles");
                throw;
            }
        }

        public async Task<KnowledgeBaseArticleDetailDto> GetArticleDetailsAsync(int id, bool trackView = false)
        {
            try
            {
                var article = await _articleRepository.GetAllAsync(a => a.Attachments, s => s.Comments, q => q.Author).Where(a => a.Id == id).ToListAsync();

                var articleDetail = _mapper.Map<KnowledgeBaseArticleDetailDto>(article.FirstOrDefault());

                if (articleDetail != null && trackView)
                {
                    await IncrementArticleViewCountAsync(id);
                }

                return articleDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving article details for ID: {id}");
                throw;
            }
        }

        public async Task<int> CreateArticleAsync(KnowledgeBaseArticleCreateDto articleDto)
        {
            try
            {
                var article = _mapper.Map<KnowledgeBaseArticle>(articleDto);
                article.CreatedDate = DateTime.UtcNow;
                article.LastUpdated = DateTime.UtcNow;
                article.ViewCount = 0;
                article.HelpfulnessRating = 0;

                await _articleRepository.AddAsync(article);
                await _articleRepository.SaveChangesAsync();

                _logger.LogInformation($"Created new knowledge base article with ID: {article.Id}");
                return article.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new article");
                throw;
            }
        }

        public async Task UpdateArticleAsync(KnowledgeBaseArticleUpdateDto articleDto)
        {
            try
            {
                var existingArticle = await _articleRepository.GetByIdAsync(articleDto.Id);
                if (existingArticle == null)
                {
                    throw new KeyNotFoundException($"Article with ID {articleDto.Id} not found");
                }

                _mapper.Map(articleDto, existingArticle);
                existingArticle.LastUpdated = DateTime.UtcNow;

                _articleRepository.Update(existingArticle);
                await _articleRepository.SaveChangesAsync();

                _logger.LogInformation($"Updated knowledge base article with ID: {articleDto.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating article with ID: {articleDto.Id}");
                throw;
            }
        }

        public async Task TogglePublishStatusAsync(int id, bool isPublished)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                if (article == null)
                {
                    throw new KeyNotFoundException($"Article with ID {id} not found");
                }

                article.IsPublished = isPublished;
                article.LastUpdated = DateTime.UtcNow;

                _articleRepository.Update(article);
                await _articleRepository.SaveChangesAsync();

                _logger.LogInformation($"{(isPublished ? "Published" : "Unpublished")} article with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error toggling publish status for article ID: {id}");
                throw;
            }
        }

        public async Task AddArticleAttachmentAsync(int articleId, KBArticleAttachmentCreateDto attachmentDto)
        {
            try
            {
                var attachment = _mapper.Map<KBArticleAttachment>(attachmentDto);
                attachment.ArticleId = articleId;

                await _attachmentRepository.AddAsync(attachment);
                await _attachmentRepository.SaveChangesAsync();

                _logger.LogInformation($"Added attachment to article ID: {articleId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding attachment to article ID: {articleId}");
                throw;
            }
        }

        public async Task AddArticleCommentAsync(int articleId, KBArticleCommentCreateDto commentDto)
        {
            try
            {
                var comment = _mapper.Map<KBArticleComment>(commentDto);
                comment.ArticleId = articleId;
                comment.CreatedDate = DateTime.UtcNow;

                await _commentRepository.AddAsync(comment);
                await _commentRepository.SaveChangesAsync();

                _logger.LogInformation($"Added comment to article ID: {articleId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding comment to article ID: {articleId}");
                throw;
            }
        }

        public async Task RateArticleHelpfulnessAsync(int articleId, int rating)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(articleId);
                if (article == null)
                {
                    throw new KeyNotFoundException($"Article with ID {articleId} not found");
                }

                // Simple average calculation
                article.HelpfulnessRating = (article.HelpfulnessRating + rating) / 2;
                article.LastUpdated = DateTime.UtcNow;

                _articleRepository.Update(article);
                await _articleRepository.SaveChangesAsync();

                _logger.LogInformation($"Updated helpfulness rating for article ID: {articleId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error rating article with ID: {articleId}");
                throw;
            }
        }

        public async Task<IEnumerable<KnowledgeBaseArticleDto>> SearchArticlesAsync(string searchTerm)
        {
            try
            {
                Expression<Func<KnowledgeBaseArticle, bool>> searchFilter = a =>
                    a.IsPublished &&
                    (a.Title.Contains(searchTerm) ||
                     a.Content.Contains(searchTerm) ||
                     a.Tags.Contains(searchTerm));

                var articles = await _articleRepository.GetAllAsync(a => a.Author).Where(searchFilter)
                    .OrderByDescending(a => a.CreatedDate).ToListAsync();

                return _mapper.Map<IEnumerable<KnowledgeBaseArticleDto>>(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error searching articles with term: {searchTerm}");
                throw;
            }
        }

        public async Task<IEnumerable<KnowledgeBaseArticleDto>> GetArticlesByCategoryAsync(string category)
        {
            try
            {
                var articles = await _articleRepository.GetAllAsync(a => a.Author).Where(a => a.IsPublished && a.Category == category)
                   .OrderByDescending(a => a.CreatedDate).ToListAsync();

                return _mapper.Map<IEnumerable<KnowledgeBaseArticleDto>>(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving articles for category: {category}");
                throw;
            }
        }

        public async Task<IEnumerable<KBArticleCategoryDto>> GetArticleCategoriesAsync()
        {
            try
            {
                var categories = await _articleRepository.GetAllAsync().Where(a => a.IsPublished).Select(a => a.Category).ToListAsync();

                return categories
                    .Distinct()
                    .Select(c => new KBArticleCategoryDto { Name = c })
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving article categories");
                throw;
            }
        }

        private async Task IncrementArticleViewCountAsync(int articleId)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article != null)
            {
                article.ViewCount++;
                _articleRepository.Update(article);
                await _articleRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<KnowledgeBaseArticleDto>> GetAllArticlesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<KnowledgeBaseArticleDto> GetArticleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }





        public Task DeleteArticleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
