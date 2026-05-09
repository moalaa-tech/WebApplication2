using AutoMapper;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;
using CRM.WebApp.UnitOfWork;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public class DemandPlanService : IDemandPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DemandPlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DemandPlanDto> GetDemandPlanByIdAsync(int id)
        {
            //var spec = new DemandPlanWithItemsSpecification(id);
            var demandPlan = await _unitOfWork.Repository<DemandPlan>().GetByIdAsync(id);

            return _mapper.Map<DemandPlanDto>(demandPlan);
        }

        public async Task<IReadOnlyList<DemandPlanListDto>> GetAllDemandPlansAsync()
        {
            //var spec = new DemandPlansWithItemsSpecification();
            var demandPlans = _unitOfWork.Repository<DemandPlan>().GetAll();

            return _mapper.Map<IReadOnlyList<DemandPlanListDto>>(demandPlans);
        }

        public async Task<DemandPlanDto> CreateDemandPlanAsync(DemandPlanCreateDto demandPlanCreateDto)
        {
            var demandPlan = _mapper.Map<DemandPlan>(demandPlanCreateDto);

            await _unitOfWork.Repository<DemandPlan>().AddAsync(demandPlan);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DemandPlanDto>(demandPlan);
        }

        public async Task UpdateDemandPlanAsync(int id, DemandPlanUpdateDto demandPlanUpdateDto)
        {
            var demandPlan = await _unitOfWork.Repository<DemandPlan>().GetByIdAsync(id);
            _mapper.Map(demandPlanUpdateDto, demandPlan);

            _unitOfWork.Repository<DemandPlan>().Update(demandPlan);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteDemandPlanAsync(int id)
        {
            var demandPlan = await _unitOfWork.Repository<DemandPlan>().GetByIdAsync(id);

            _unitOfWork.Repository<DemandPlan>().Delete(demandPlan);
            await _unitOfWork.CompleteAsync();
        }

        public Task<bool> AddItemToPlanAsync(int planId, DemandPlanItemCreateDto itemDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePlanItemAsync(DemandPlanItemDto itemDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveItemFromPlanAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemProcurementStatusAsync(int itemId, bool isProcured)
        {
            throw new NotImplementedException();
        }

        public Task<SupplyChainDashboardDto> GetDashboardDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DemandPlanItemDto>> GetPlanItemsAsync(int planId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ForecastDataDto>> GetForecastDataAsync(int planId, string dataType = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GenerateForecastAsync(int planId)
        {
            throw new NotImplementedException();
        }
    }
}
