using AutoMapper;
using CRM.Domain.Entities.SalesManagement;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Notification;
using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ActivityType = CRM.Domain.Enums.ActivityType;

namespace CRM.WebApp.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<Lead> _leadRepository;
        private readonly IRepository<Opportunity> _opportunityRepository;
        private readonly IRepository<Deal> _dealRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _userService;
        private readonly INotificationService _notificationService;

        public ActivityService(
            IRepository<Activity> activityRepository,
            IRepository<Lead> leadRepository,
            IRepository<Opportunity> opportunityRepository,
            IRepository<Deal> dealRepository,
            IMapper mapper,
            IAuthenticationService userService,
            INotificationService notificationService)
        {
            _activityRepository = activityRepository;
            _leadRepository = leadRepository;
            _opportunityRepository = opportunityRepository;
            _dealRepository = dealRepository;
            _mapper = mapper;
            _userService = userService;
            _notificationService = notificationService;
        }

        public async Task<ActivityDto> GetActivityByIdAsync(int id)
        {
            var activity = await _activityRepository.GetAsync(a => a.Id == id, a => a.Include(z => z.Lead).Include(s => s.Opportunity).Include(a => a.OwnerUser));
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            var activities = await _activityRepository.GetAllAsync(includes: a => new { a.OwnerUser, a.Lead, a.Opportunity, a.Deal }).ToListAsync();

            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<PaginatedList<ActivityDto>> GetActivitiesPaginatedAsync(int pageNumber, int pageSize, ActivityFilterDto filter)
        {
            Expression<Func<Activity, bool>> predicate = a =>
                (string.IsNullOrEmpty(filter.SearchTerm) ||
                 a.Subject.Contains(filter.SearchTerm) ||
                 a.Description.Contains(filter.SearchTerm)) &&
                // (!filter.Type.HasValue || a.Type == filter.Type) &&
                //(!filter.Status.HasValue || a.Status == filter.Status) &&
                (!filter.FromDate.HasValue || a.DueDate >= filter.FromDate) &&
                (!filter.ToDate.HasValue || a.DueDate <= filter.ToDate) &&
                (filter.OwnerUserId > 0 || a.OwnerUserId == filter.OwnerUserId);

            var activities = await _activityRepository.GetPaginatedAsync(
                pageNumber,
                pageSize,
                predicate,
                orderBy: a => a.OrderBy(a => a.DueDate),
                includes: a => a
                    .Include(a => a.OwnerUser)
                    .Include(a => a.Lead)
                    .Include(a => a.Opportunity)
                    .Include(a => a.Deal));

            return _mapper.Map<PaginatedList<ActivityDto>>(activities);
        }

        public async Task<IEnumerable<ActivityDto>> GetUpcomingActivitiesAsync(int daysAhead)
        {
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddDays(daysAhead);

            var activities = await _activityRepository.GetAllAsync(a => a.OwnerUser).Where(a => a.DueDate >= startDate && a.DueDate <= endDate && a.Status != ActivityStatus.Completed).OrderBy(a => a.DueDate).ToListAsync();

            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesByEntityAsync(ActivityType entityType, int entityId)
        {
            Expression<Func<Activity, bool>> filter = entityType switch
            {
                //ActivityType.Lead => a => a.LeadId == entityId,
                //ActivityType.Opportunity => a => a.OpportunityId == entityId,
                //Domain.Enums.EntityType.Deal => a => a.DealId == entityId,_ => a => true
            };

            var activities = await _activityRepository.GetAllAsync(a => a.OwnerUser).Where(filter).OrderByDescending(a => a.DueDate).ToListAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<int> CreateActivityAsync(CreateActivityDto model)
        {
            var activity = _mapper.Map<Activity>(model);
            activity.Status = ActivityStatus.NotStarted;

            await _activityRepository.AddAsync(activity);

            // Send notification to owner
            if (activity.OwnerUserId > 0)
            {
                CreateNotificationDto notification = new CreateNotificationDto();
                //await _notificationService.CreateNotificationAsync(
                //    activity.OwnerUserId,
                //    "New Activity Assigned",
                //    $"You have a new activity: {activity.Subject} due on {activity.DueDate:MMM dd, yyyy}",
                //    NotificationType.ActivityAssigned,
                //    activity.Id);
            }

            return activity.Id;
        }

        public async Task UpdateActivityAsync(UpdateActivityDto model)
        {
            var activity = await _activityRepository.GetByIdAsync(model.Id);
            _mapper.Map(model, activity);
            _activityRepository.Update(activity);
        }

        public async Task DeleteActivityAsync(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            _activityRepository.Delete(activity);
        }

        public async Task CompleteActivityAsync(int id, string outcomeNotes)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            activity.Status = ActivityStatus.Completed;
            activity.CompletedDate = DateTime.UtcNow;
            activity.Description = string.IsNullOrEmpty(outcomeNotes) ? activity.Description : $"{activity.Description}\n\nOutcome: {outcomeNotes}";
            _activityRepository.Update(activity);
        }

        public async Task<ActivityDto> GetActivityDtoForEditAsync(int id)
        {
            var activity = await GetActivityByIdAsync(id);
            var model = _mapper.Map<ActivityDto>(activity);
            await PrepareActivityDto(model);
            return model;
        }

        public async Task<ActivityDto> GetActivityViewModelForCreateAsync(ActivityType? entityType = null, int? entityId = null)
        {
            var model = new ActivityDto
            {
                DueDate = DateTime.UtcNow.AddDays(1),
                Type = ActivityType.Task,
                Status = ActivityStatus.NotStarted
            };

            if (entityType.HasValue && entityId.HasValue)
            {
                //switch (entityType.Value)
                //{
                //    case Domain.Enums.EntityType.Lead:
                //        model.LeadId = entityId;
                //        break;
                //    case Domain.Enums.EntityType.Opportunity:
                //        model.OpportunityId = entityId;
                //        break;
                //    case Domain.Enums.EntityType.Deal:
                //        model.DealId = entityId;
                //        break;
                //}
            }

            await PrepareActivityDto(model);
            return model;
        }

        private async Task PrepareActivityDto(ActivityDto model)
        {
            //    model.Users = await _userService.GetUsersSelectListAsync();


            //    model.Types = Enum.GetValues(typeof(ActivityType)).Cast<ActivityType>()
            //        .Select(t => new SelectListItem(t.ToString(), ((int)t).ToString()));

            //    model.Statuses = Enum.GetValues(typeof(ActivityStatus)).Cast<ActivityStatus>()
            //        .Select(s => new SelectListItem(s.ToString(), ((int)s).ToString()));
        }

        public async Task<ActivityStatsDto> GetActivityStatsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            startDate ??= DateTime.UtcNow.AddMonths(-1);
            endDate ??= DateTime.UtcNow;

            var activities = await _activityRepository.GetAllAsync().Where(a => a.DueDate >= startDate && a.DueDate <= endDate).ToListAsync();

            return new ActivityStatsDto
            {
                TotalActivities = activities.Count,
                CompletedActivities = activities.Count(a => a.Status == ActivityStatus.Completed),
                OverdueActivities = activities.Count(a => a.DueDate < DateTime.UtcNow && a.Status != ActivityStatus.Completed),
                ActivitiesByType = activities.GroupBy(a => a.Type).ToDictionary(g => g.Key.ToString(), g => g.Count()),
                CompletionRate = activities.Any()
                    ? (decimal)activities.Count(a => a.Status == ActivityStatus.Completed) / activities.Count * 100
                    : 0
            };
        }

        public Task<ActivityDto> GetActivityViewModelForEditAsync(int id)
        {
            throw new NotImplementedException();
        }



        Task<ActivityDto> IActivityService.GetActivityStatsAsync(DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetUsersSelectListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
