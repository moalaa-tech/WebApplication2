using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.ViewModels.MarketingAutomation;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface ISocialMediaService
    {
        Task<IEnumerable<SocialMediaPostViewModel>> GetAllPostsAsync();
        Task<SocialMediaPostViewModel> GetPostByIdAsync(int id);
        Task CreatePostAsync(SocialMediaPostDto postDto, string userId);
        Task UpdatePostAsync(int id, SocialMediaPostDto postDto, string userId);
        Task DeletePostAsync(int id);
        Task SchedulePostAsync(int id);
    }
}
