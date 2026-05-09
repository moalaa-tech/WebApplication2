using AutoMapper;
using CRM.Domain.Entities.CustomerService;
using CRM.Domain.Enums.CustomerService;
using CRM.WebApp.DTOs.CustomerService;
using CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle;
using CRM.WebApp.DTOs.CustomerService.ServiceRequest;
using CRM.WebApp.DTOs.CustomerService.Ticket;
using CRM.WebApp.ViewModels.CustomerService;
using CRM.WebApp.ViewModels.CustomerService.ServiceLevelAgreement;
using ServiceRequestDto = CRM.WebApp.DTOs.CustomerService.ServiceRequest.ServiceRequestDto;

namespace CRM.WebApp.MappingProfiles
{
    public class CustomerServiceProfile : Profile
    {
        public CustomerServiceProfile()
        {
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<ServiceRequest, ServiceRequestDto>().ReverseMap();
            CreateMap<ServiceLevelAgreement, SLADto>().ReverseMap();
            CreateMap<KnowledgeBaseArticle, KnowledgeBaseArticleDto>().ReverseMap();

            // ViewModel mappings can be added here as well

            CreateMap<SupportAgent, SupportAgentDetailsViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<SupportAgentCreateViewModel, SupportAgent>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.AuthId, opt => opt.Ignore());

            CreateMap<SupportAgentEditViewModel, SupportAgent>()
                .IncludeBase<SupportAgentCreateViewModel, SupportAgent>();

            CreateMap<Ticket, SupportAgentTicketViewModel>()
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TicketNumber, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));



            CreateMap<SupportAgent, SupportAgentListViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.OpenTickets, opt => opt.MapFrom(src => src.AssignedTickets.Count(t => t.Status != TicketStatus.Closed && t.Status != TicketStatus.Resolved)));

            CreateMap<SupportAgentCreateViewModel, SupportAgent>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()));

            CreateMap<SupportAgentEditViewModel, SupportAgent>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()));

            CreateMap<SupportAgent, SupportAgentDetailViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.RecentTickets, opt => opt.MapFrom(src => src.AssignedTickets
                    .OrderByDescending(t => t.CreatedDate)
                    .Take(5)
                    .Select(t => new SupportAgentTicketViewModel
                    {
                        Id = t.Id,
                        TicketNumber = t.Id.ToString(),
                        Subject = t.Subject,
                        Status = t.Status.ToString(),
                        Priority = t.Priority.ToString(),
                        CreatedDate = t.CreatedDate
                    })));

            CreateMap<SupportAgent, SupportAgentEditViewModel>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => string.Join(", ", src.Skills)));


            CreateMap<Ticket, TicketViewModel>()
                .ForMember(a => a.CustomerName, s => s.MapFrom(m => m.Customer.Name))
                .ForMember(a => a.AssignedUserName, s => s.MapFrom(m => $"{m.AssignedAgent.FirstName} {m.AssignedAgent.LastName}"))
                .ReverseMap();

            CreateMap<CreateTicketViewModel, TicketDto>().ReverseMap();

            CreateMap<UpdateTicketViewModel, TicketDto>().ReverseMap();




            // DTO to Entity
            CreateMap<SLADto, ServiceLevelAgreement>().ReverseMap();
            CreateMap<SLAMetricDto, SLAMetrics>().ReverseMap();

            // Create DTO to Entity
            CreateMap<SLACreateDto, ServiceLevelAgreement>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Metrics, opt => opt.Ignore());

            // Update DTO to Entity
            CreateMap<SLAUpdateDto, ServiceLevelAgreement>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity to ViewModel
            CreateMap<ServiceLevelAgreement, ServiceLevelAgreementViewModel>();
            CreateMap<SLAMetrics, SLAMetricsViewModel>();

            // ViewModel to DTO
            CreateMap<CreateServiceLevelAgreementViewModel, SLADto>();
            CreateMap<EditServiceLevelAgreementViewModel, SLAUpdateDto>();


        }
    }
}
