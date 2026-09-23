using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.SalesManagement;
using CRM.WebApp.ViewModels.SalesManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    // Controllers/ActivitiesController.cs
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly ILeadService _leadService;
        private readonly IOpportunityService _opportunityService;
        private readonly IDealService _dealService;
        private readonly IMapper Mapper;

        public ActivitiesController(
            IActivityService activityService,
            ILeadService leadService,
            IOpportunityService opportunityService,
            IMapper mapper,
            IDealService dealService)
        {
            _activityService = activityService;
            _leadService = leadService;
            _opportunityService = opportunityService;
            _dealService = dealService;
            Mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? page, [FromQuery] string? searchTerm, [FromQuery] ActivityType? type, [FromQuery] ActivityStatus? status,
            [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? ownerUserId)
        {
            var filter = new ActivityFilterViewModel
            {
                SearchTerm = searchTerm,
                Type = type,
                Status = status,
                FromDate = fromDate,
                ToDate = toDate,
                OwnerUserId = int.Parse(ownerUserId)
            };

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var ActivityFilterDto = Mapper.Map<ActivityFilterDto>(filter);

            var activities = await _activityService.GetActivitiesPaginatedAsync(pageNumber, pageSize, ActivityFilterDto);

            var model = new ActivityListViewModel
            {
                Activities = activities,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = activities.TotalCount
                },
                Filter = filter,
                Users = await _activityService.GetUsersSelectListAsync()
            };

            return Ok(model);
        }

        [HttpGet("Upcoming")]
        public async Task<IActionResult> Upcoming([FromQuery] int daysAhead = 7)
        {
            var activities = await _activityService.GetUpcomingActivitiesAsync(daysAhead);
            return Ok(activities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CreateActivityDto = Mapper.Map<CreateActivityDto>(model);
                await _activityService.CreateActivityAsync(CreateActivityDto);

                return Ok();
            }

            var ActivityViewModel = Mapper.Map<ActivityViewModel>(model);

            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateActivityViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdateActivityDto = Mapper.Map<UpdateActivityDto>(model);

                await _activityService.UpdateActivityAsync(UpdateActivityDto);
                return Ok();
            }

            var ActivityViewModel = Mapper.Map<ActivityViewModel>(model);

            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _activityService.DeleteActivityAsync(id);
            return Ok();
        }

        [HttpPost("Complete")]
        public async Task<IActionResult> Complete([FromQuery] int id, [FromBody] CompleteActivityViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _activityService.CompleteActivityAsync(model.Id, model.OutcomeNotes);
                return Ok();
            }

            return Ok(model);
        }
    }
}