using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.Services.MarketingAutomation;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    public class SocialMediaIntegrationController : Controller
    {

        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaIntegrationController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _socialMediaService.GetAllPostsAsync();
            return View(posts);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _socialMediaService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMediaPostDto postDto)
        {
            if (ModelState.IsValid)
            {
                await _socialMediaService.CreatePostAsync(postDto, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(postDto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _socialMediaService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialMediaPostDto postDto)
        {
            if (id != postDto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _socialMediaService.UpdatePostAsync(id, postDto, User.Identity.Name);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _socialMediaService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _socialMediaService.DeletePostAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule(int id)
        {
            try
            {
                await _socialMediaService.SchedulePostAsync(id);
                TempData["SuccessMessage"] = "Post scheduled successfully!";
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
