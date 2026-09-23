using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.Services.MarketingAutomation;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.MarketingAutomation
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialMediaIntegrationController : ControllerBase
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
            return Ok(posts);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _socialMediaService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SocialMediaPostDto postDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _socialMediaService.CreatePostAsync(postDto, User.Identity.Name);
            return Ok(postDto);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SocialMediaPostDto postDto)
        {
            if (id != postDto.Id) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _socialMediaService.UpdatePostAsync(id, postDto, User.Identity.Name);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok(postDto);
        }

        [HttpDelete("{id:int}")]
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
            return Ok();
        }

        [HttpPost("Schedule/{id:int}")]
        public async Task<IActionResult> Schedule(int id)
        {
            try
            {
                await _socialMediaService.SchedulePostAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
