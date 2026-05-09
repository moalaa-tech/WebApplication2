using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.EmailIntegration;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public class MarketingAutomationService : IMarketingAutomationService
    {
        private readonly IRepository<Automation> AutomationRepository;
        private readonly IRepository<AutomationStep> AutomationStepRepository;
        private readonly IRepository<EmailTemplate> EmailTemplateRepo;
        private readonly IRepository<Campaign> CampaignRepo;
        private readonly IRepository<CampaignContact> CampaignContactRepo;
        private readonly IRepository<Interaction> InteractionRepo;
        private readonly IRepository<Contact> ContactRepository;
        private readonly IRepository<AutomationContact> AutomationContactRepo;
        private readonly IEmailService _emailService;
        private readonly ILogger<MarketingAutomationService> _logger;
        private readonly IMapper _mapper;

        public MarketingAutomationService(
            IRepository<Automation> repository,
            IRepository<Contact> contactRepository,
            IRepository<AutomationContact> automationContactRepo,
            IEmailService emailService,
            IMapper mapper,
            IRepository<Interaction> interactionRepo,
            IRepository<CampaignContact> campaignContactRepo,
            IRepository<Campaign> campaignRepo,
            IRepository<EmailTemplate> emailTemplateRepo,
            IRepository<AutomationStep> automationStepRepository,
            ILogger<MarketingAutomationService> logger)
        {
            AutomationRepository = repository;
            _emailService = emailService;
            _logger = logger;
            ContactRepository = contactRepository;
            AutomationContactRepo = automationContactRepo;
            _mapper = mapper;
            AutomationStepRepository = automationStepRepository;
            EmailTemplateRepo = emailTemplateRepo;
            CampaignRepo = campaignRepo;
            CampaignContactRepo = campaignContactRepo;
            InteractionRepo = interactionRepo;
        }

        public async Task RunAutomationForContact(int automationId, int contactId)
        {
            var automation = await AutomationRepository.GetAsync(a => a.Id == automationId, q => q.Include(x => x.Steps));

            var contact = await ContactRepository.GetByIdAsync(contactId);

            if (automation == null || contact == null)
            {
                _logger.LogWarning($"Automation {automationId} or contact {contactId} not found");
                return;
            }

            // Check if contact is already in this automation
            var existing = await AutomationContactRepo.GetAsync(ac => ac.AutomationId == automationId && ac.ContactId == contactId);

            if (existing != null)
            {
                _logger.LogInformation($"Contact {contactId} already in automation {automationId}");
                return;
            }

            // Add contact to automation
            AutomationContact automationContact = new AutomationContact
            {
                AutomationId = automationId,
                ContactId = contactId,
                CurrentStep = 1
            };

            await AutomationContactRepo.AddAsync(automationContact);
            await AutomationContactRepo.SaveChangesAsync();


            // Execute first step
            var firstStep = automation.Steps.OrderBy(s => s.Order).FirstOrDefault();
            if (firstStep != null)
            {
                await ExecuteStep(firstStep, contact);
            }

            automation.LastRunDate = DateTime.UtcNow;
            await AutomationRepository.SaveChangesAsync();
        }

        public async Task ProcessTrigger(AutomationTrigger trigger, int contactId, string customEvent = null)
        {
            var automations = await AutomationRepository.GetAllAsync(a => a.IsActive && a.Trigger == trigger).ToListAsync();

            foreach (var automation in automations)
            {
                if (trigger == AutomationTrigger.CustomEvent && !string.IsNullOrEmpty(automation.CustomEventName) && automation.CustomEventName != customEvent)
                {
                    continue;
                }

                await RunAutomationForContact(automation.Id, contactId);
            }
        }

        public async Task ExecuteAutomationStep(int automationId, int contactId, int stepId)
        {
            var step = await AutomationStepRepository.GetByIdAsync(stepId);
            var contact = await ContactRepository.GetByIdAsync(contactId);

            if (step != null && contact != null)
            {
                await ExecuteStep(step, contact);
            }
        }

        private async Task ExecuteStep(AutomationStep step, Contact contact)
        {
            switch (step.Action)
            {
                case AutomationAction.SendEmail:
                    if (step.EmailTemplateId.HasValue)
                    {
                        var template = await EmailTemplateRepo.GetByIdAsync(step.EmailTemplateId.Value);
                        if (template != null)
                        {
                            await _emailService.SendTemplateEmail(template, contact.Email, contact);
                            await RecordInteraction(contact.Id, null, InteractionType.EmailSent, $"Automation email sent: {template.Name}");
                        }
                    }
                    break;

                case AutomationAction.ChangeStatus:
                    if (!string.IsNullOrEmpty(step.StatusValue))
                    {
                        contact.Status = Enum.Parse<ContactStatus>(step.StatusValue);
                        ContactRepository.Update(contact);
                        await ContactRepository.SaveChangesAsync();
                        await RecordInteraction(contact.Id, null, InteractionType.Note, $"Status changed to {step.StatusValue} by automation");
                    }
                    break;

                case AutomationAction.AddToCampaign:
                    if (step.CampaignId.HasValue)
                    {
                        var campaign = await CampaignRepo.GetByIdAsync(step.CampaignId.Value);
                        if (campaign != null)
                        {
                            var existing = await CampaignContactRepo.GetAsync(cc => cc.CampaignId == campaign.Id && cc.ContactId == contact.Id);

                            if (existing == null)
                            {
                                await CampaignContactRepo.AddAsync(new CampaignContact
                                {
                                    CampaignId = campaign.Id,
                                    ContactId = contact.Id,
                                });

                                await RecordInteraction(contact.Id, campaign.Id, InteractionType.Note, $"Added to campaign {campaign.Name} by automation");
                            }
                        }
                    }
                    break;

                case AutomationAction.Wait:
                    // For wait steps, we just update the current step in AutomationContact
                    // A background job will handle moving to the next step after the wait period
                    break;

                    // Implement other action types...
            }

            await CampaignContactRepo.SaveChangesAsync();
        }

        private async Task RecordInteraction(int? contactId, int? campaignId, InteractionType type, string details)
        {
            var interaction = new Interaction
            {
                ContactId = contactId,
                CampaignId = campaignId,
                Type = type,
                Details = details,
                InteractionDate = DateTime.UtcNow
            };

            await InteractionRepo.AddAsync(interaction);
            await InteractionRepo.SaveChangesAsync();
        }

        public async Task<List<Automation>> GetActiveAutomations()
        {
            return await AutomationRepository.GetAllAsync(a => a.IsActive).Include(a => a.Steps).ToListAsync();
        }
    }
}
