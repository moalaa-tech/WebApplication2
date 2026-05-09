using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.SalesManagement;
using CRM.WebApp.ViewModels.SalesManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers
{
    // Controllers/ActivitiesController.cs
    [Authorize]
    public class ActivitiesController : Controller
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
        public async Task<IActionResult> Index(int? page, string searchTerm, ActivityType? type, ActivityStatus? status,
            DateTime? fromDate, DateTime? toDate, string ownerUserId)
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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Upcoming(int daysAhead = 7)
        {
            var activities = await _activityService.GetUpcomingActivitiesAsync(daysAhead);
            return View(activities);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Domain.Enums.EntityType? entityType, int? entityId)
        {
            //var model = await _activityService.GetActivityViewModelForCreateAsync(entityType, entityId);
            return View();
        }

        // POST: Activities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CreateActivityDto = Mapper.Map<CreateActivityDto>(model);
                var activityId = await _activityService.CreateActivityAsync(CreateActivityDto);

                if (model.LeadId.HasValue)
                    return RedirectToAction("Details", "Leads", new { id = model.LeadId });
                if (model.OpportunityId.HasValue)
                    return RedirectToAction("Details", "Opportunities", new { id = model.OpportunityId });
                if (model.DealId.HasValue)
                    return RedirectToAction("Details", "Deals", new { id = model.DealId });

                return RedirectToAction(nameof(Details), new { id = activityId });
            }

            var ActivityViewModel = Mapper.Map<ActivityViewModel>(model);

            await PrepareActivityViewModel(ActivityViewModel);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _activityService.GetActivityViewModelForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateActivityViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdateActivityDto = Mapper.Map<UpdateActivityDto>(model);

                await _activityService.UpdateActivityAsync(UpdateActivityDto);
                return RedirectToAction(nameof(Details), new { id });
            }

            var ActivityViewModel = Mapper.Map<ActivityViewModel>(model);

            await PrepareActivityViewModel(ActivityViewModel);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _activityService.DeleteActivityAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Complete(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            var model = new CompleteActivityViewModel
            {
                Id = activity.Id,
                Subject = activity.Subject,
                DueDate = activity.DueDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id, CompleteActivityViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _activityService.CompleteActivityAsync(model.Id, model.OutcomeNotes);
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(model);
        }

        private async Task PrepareActivityViewModel(ActivityViewModel model)
        {
            if (model.LeadId.HasValue)
            {
                var lead = await _leadService.GetLeadByIdAsync(model.LeadId.Value);
                ViewData["EntityName"] = $"{lead.FirstName} {lead.LastName}";
                ViewData["EntityType"] = "Lead";
            }
            else if (model.OpportunityId.HasValue)
            {
                var opportunity = await _opportunityService.GetOpportunityByIdAsync(model.OpportunityId.Value);
                ViewData["EntityName"] = opportunity.Title;
                ViewData["EntityType"] = "Opportunity";
            }
            else if (model.DealId.HasValue)
            {
                var deal = await _dealService.GetDealByIdAsync(model.DealId.Value);
                ViewData["EntityName"] = deal.Name;
                ViewData["EntityType"] = "Deal";
            }
        }
    }
}
