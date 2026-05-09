using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.Repositories;
using CRM.WebApp.ViewModels.MarketingAutomation;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IRepository<SocialMediaPost> _repository;
        private readonly IMapper _mapper;

        public SocialMediaService(IRepository<SocialMediaPost> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SocialMediaPostViewModel>> GetAllPostsAsync()
        {
            var posts = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<SocialMediaPostViewModel>>(posts);
        }

        public async Task<SocialMediaPostViewModel> GetPostByIdAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            return _mapper.Map<SocialMediaPostViewModel>(post);
        }

        public async Task CreatePostAsync(SocialMediaPostDto postDto, string userId)
        {
            var post = _mapper.Map<SocialMediaPost>(postDto);
            //post.CreatedBy = userId;
            post.DateCreated = DateTime.UtcNow;

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(int id, SocialMediaPostDto postDto, string userId)
        {
            var existingPost = await _repository.GetByIdAsync(id);
            if (existingPost == null) throw new KeyNotFoundException("Post not found");

            _mapper.Map(postDto, existingPost);
            //existingPost.ModifiedBy = userId;
            existingPost.DateModified = DateTime.UtcNow;

            _repository.Update(existingPost);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null) throw new KeyNotFoundException("Post not found");

            _repository.Delete(post);
            await _repository.SaveChangesAsync();
        }

        public async Task SchedulePostAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null) throw new KeyNotFoundException("Post not found");

            post.Status = PostStatus.Scheduled;
            _repository.Update(post);
            await _repository.SaveChangesAsync();

            // Here you would integrate with actual social media APIs
            // This would typically be in a separate background service
        }
    }
}
