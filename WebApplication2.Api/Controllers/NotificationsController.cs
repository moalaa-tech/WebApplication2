using AutoMapper;
using CRM.WebApp.DTOs.Notification;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication2.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        // GET: Notifications
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        // GET: Notifications/Details/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // POST: Notifications/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto dto)
        {
            if (ModelState.IsValid)
            {
                dto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _notificationService.CreateNotificationAsync(dto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: Notifications/Edit/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateNotificationDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _notificationService.UpdateNotificationAsync(dto);
                }
                catch (Exception)
                {
                    if (!await NotificationExists(dto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE: Notifications/Delete/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return Ok();
        }

        // POST: Notifications/MarkAsRead/5
        [HttpPost("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead([FromQuery] int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok();
        }

        private async Task<bool> NotificationExists(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            return notification != null;
        }
    }
}