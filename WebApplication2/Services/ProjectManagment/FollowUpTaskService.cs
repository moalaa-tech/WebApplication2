using AutoMapper;
using CRM.Domain.Entities.ProjectManagment;
using CRM.WebApp.DTOs.FollowUpTask;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.ProjectManagment
{
    public class FollowUpTaskService : IFollowUpTaskService
    {
        private readonly IRepository<FollowUpTask> _repository;
        private readonly IMapper _mapper;

        public FollowUpTaskService(IRepository<FollowUpTask> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FollowUpTaskDto>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllAsync().ToListAsync();
            return _mapper.Map<IEnumerable<FollowUpTaskDto>>(tasks);
        }

        public async Task<FollowUpTaskDto> GetTaskByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            return _mapper.Map<FollowUpTaskDto>(task);
        }

        public async Task<bool> CreateTaskAsync(CreateFollowUpTaskDto taskDto)
        {
            var task = _mapper.Map<FollowUpTask>(taskDto);
            await _repository.AddAsync(task);
            await _repository.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateTaskAsync(UpdateFollowUpTaskDto taskDto)
        {
            var task = await _repository.GetByIdAsync(taskDto.Id);
            if (task != null)
            {
                _mapper.Map(taskDto, task);
                _repository.Update(task);
                await _repository.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            _repository.Delete(task);
            return true;
        }

        public async Task<FollowUpTaskDto> MarkComplete(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return null;


            task.Completed = true;
            // Auto-create next task if recurring
            if (task.IsRecurring && task.RecurrenceDays.HasValue)
            {
                task.DueDate = DateTime.Today.AddDays(task.RecurrenceDays.Value);
                task.IsRecurring = true;
                task.RecurrenceDays = task.RecurrenceDays;

                _repository.Update(task);
                await _repository.SaveChangesAsync();
                return _mapper.Map<FollowUpTaskDto>(task);
            }
            return null;
        }
    }
}
