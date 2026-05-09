using AutoMapper;
using CRM.Domain.Entities.CustomerService;
using CRM.Domain.Enums.CustomerService;
using CRM.WebApp.DTOs.CustomerService;
using CRM.WebApp.DTOs.CustomerService.ServiceRequest;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using ServiceRequestDto = CRM.WebApp.DTOs.CustomerService.ServiceRequest.ServiceRequestDto;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IRepository<ServiceRequest> _requestRepository;
        private readonly IRepository<ServiceRequestAttachment> _attachmentRepository;
        private readonly IMapper _mapper;

        public ServiceRequestService(
            IRepository<ServiceRequest> requestRepository,
            IRepository<ServiceRequestAttachment> attachmentRepository,
            IMapper mapper)
        {
            _requestRepository = requestRepository;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceRequestDto>> GetAllServiceRequestsAsync()
        {
            var requests = await _requestRepository.GetAllAsync(a => a.Customer, s => s.AssignedAgent).ToListAsync();
            return _mapper.Map<IEnumerable<ServiceRequestDto>>(requests);
        }

        public async Task<ServiceRequestDto> GetServiceRequestByIdAsync(int id)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            return _mapper.Map<ServiceRequestDto>(request);
        }

        public async Task<ServiceRequestDetailDto> GetServiceRequestWithDetailsAsync(int id)
        {
            var request = await _requestRepository.GetAllAsync(a => a.Customer, s => s.AssignedAgent, q => q.Attachments).Where(r => r.Id == id)
                .ToListAsync();

            return _mapper.Map<ServiceRequestDetailDto>(request.FirstOrDefault());
        }

        public async Task<int> CreateServiceRequestAsync(ServiceRequestCreateDto requestDto)
        {
            var request = _mapper.Map<ServiceRequest>(requestDto);
            request.RequestNumber = GenerateRequestNumber();
            request.CreatedDate = DateTime.UtcNow;
            request.Status = RequestStatus.New;

            await _requestRepository.AddAsync(request);
            await _requestRepository.SaveChangesAsync();

            return request.Id;
        }

        public async Task UpdateServiceRequestAsync(ServiceRequestUpdateDto requestDto)
        {
            var request = await _requestRepository.GetByIdAsync(requestDto.Id);
            if (request == null)
                throw new KeyNotFoundException("Service request not found");

            _mapper.Map(requestDto, request);

            await _requestRepository.AddAsync(request);
            await _requestRepository.SaveChangesAsync();
        }

        public async Task AddAttachmentAsync(ServiceRequestAttachmentDto attachmentDto)
        {
            var attachment = _mapper.Map<ServiceRequestAttachment>(attachmentDto);
            attachment.UploadDate = DateTime.UtcNow;

            await _attachmentRepository.AddAsync(attachment);
            await _attachmentRepository.SaveChangesAsync();
        }

        public async Task AssignRequestToAgentAsync(int requestId, int agentId)
        {
            var request = await _requestRepository.GetByIdAsync(requestId);
            if (request == null)
                throw new KeyNotFoundException("Service request not found");

            request.AssignedTo = agentId;
            request.Status = RequestStatus.Assigned;

            await _requestRepository.AddAsync(request);
            await _requestRepository.SaveChangesAsync();
        }

        public async Task CompleteServiceRequestAsync(int requestId, string resolutionNotes)
        {
            var request = await _requestRepository.GetByIdAsync(requestId);
            if (request == null)
                throw new KeyNotFoundException("Service request not found");

            request.Status = RequestStatus.Completed;
            request.CompletionDate = DateTime.UtcNow;
            request.ResolutionNotes = resolutionNotes;

            _requestRepository.Update(request);
            await _requestRepository.SaveChangesAsync();
        }

        private string GenerateRequestNumber()
        {
            var prefix = "SR";
            var datePart = DateTime.Now.ToString("yyyyMMdd");
            var randomPart = new Random().Next(1000, 9999).ToString("D4");

            return $"{prefix}-{datePart}-{randomPart}";
        }

        public Task DeleteServiceRequestAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
