using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Campaign;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CampaignService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampaignDto>> GetAllCampaignsAsync()
        {
            var campaigns = await _unitOfWork.Campaigns.GetAllAsync(a => a.EmailTemplate).ToListAsync();
            return _mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<CampaignDto> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id);
            return _mapper.Map<CampaignDto>(campaign);
        }

        public async Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto campaignDto)
        {
            var campaign = _mapper.Map<Campaign>(campaignDto);
            await _unitOfWork.Campaigns.AddAsync(campaign);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CampaignDto>(campaign);
        }

        public async Task UpdateCampaignAsync(int id, UpdateCampaignDto campaignDto)
        {
            var existingCampaign = await _unitOfWork.Campaigns.GetByIdAsync(id);
            if (existingCampaign == null)
                throw new Exception("Campaign not found");

            _mapper.Map(campaignDto, existingCampaign);
            _unitOfWork.Campaigns.Update(existingCampaign);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCampaignAsync(int id)
        {
            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id);
            if (campaign == null)
                throw new Exception("Campaign not found");

            _unitOfWork.Campaigns.Delete(campaign);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> CampaignExistsAsync(int id)
        {
            return await _unitOfWork.Campaigns.ExistsAsync(c => c.Id == id);
        }

        public async Task ChangeCampaignStatusAsync(int id, CampaignStatus status)
        {
            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id);
            if (campaign == null)
                throw new Exception("Campaign not found");

            campaign.Status = status;
            _unitOfWork.Campaigns.Update(campaign);
            await _unitOfWork.CompleteAsync();
        }

    }
}
