using CRM.Domain.IdentityEntity;
using CRM.Identity.Services;
using CRM.WebApp.DbContext;
using CRM.WebApp.EmailIntegration;
using CRM.WebApp.Extensions;
using CRM.WebApp.Localization;
using CRM.WebApp.Middlewares;
using CRM.WebApp.Repositories;
using CRM.WebApp.Security;
using CRM.WebApp.Services;
using CRM.WebApp.Services.AssetsManagment;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.DocumentManagement;
using CRM.WebApp.Services.Finance_Accounting;
using CRM.WebApp.Services.HumanResources;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.Inventory;
using CRM.WebApp.Services.InventoryManagment;
using CRM.WebApp.Services.Lookups;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.Services.MarketingAutomation.CampaignAnalytics;
using CRM.WebApp.Services.MarketingAutomation.Segment;
using CRM.WebApp.Services.ProjectManagment;
using CRM.WebApp.Services.SalesManagement;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.Settings;
using CRM.WebApp.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using QuestPDF.Infrastructure;

ExcelPackage.License.SetNonCommercialPersonal("mohamed");
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(a => a.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

var configuration = builder.Configuration;

// API + JSON controllers. The referenced WebApplication2 MVC assembly is
// intentionally excluded so only this project's API controllers are mapped.
builder.Services.AddControllers()
    .ConfigureApplicationPartManager(pm =>
    {
        var externalParts = pm.ApplicationParts
            .Where(p => p.Name == "WebApplication2")
            .ToList();
        foreach (var part in externalParts)
        {
            pm.ApplicationParts.Remove(part);
        }
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health checks
builder.Services.AddHealthChecks();

// Localization services (JSON-based)
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();

// Add memory caching for lookup data
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "ar" };
    options.SetDefaultCulture("en")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CookieRequestCultureProvider(),
        new QueryStringRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("defaultConnection"));
});

// Configure Identity with enhanced security
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password requirements (OWASP recommendations)
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

// CORS for the Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

#region services
builder.Services.AddScoped<ISegmentService, SegmentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IOpportunityService, OpportunityService>();
builder.Services.AddScoped<IDealService, DealService>();
builder.Services.AddScoped<IFollowUpTaskService, FollowUpTaskService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IMarketingAutomationService, MarketingAutomationService>();
builder.Services.AddScoped<IAutomationService, AutomationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAutomationStepService, AutomationStepService>();
builder.Services.AddScoped<IJobTitleService, JobTitleService>();
builder.Services.AddScoped<ISalesPipelineService, SalesPipelineService>();
builder.Services.AddScoped<IDealService, DealService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();

// Add HR services
builder.Services.AddScoped<IJobPostingService, JobPostingService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
builder.Services.AddScoped<IGeneralLedgerService, GeneralLedgerService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<ISLAService, SLAService>();
builder.Services.AddScoped<IKnowledgeBaseService, KnowledgeBaseService>();
builder.Services.AddScoped<IDemandPlanService, DemandPlanService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJobPhaseService, JobPhaseService>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();
builder.Services.AddScoped<ILeadScoreService, LeadScoreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentCategoryService, DocumentCategoryService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IDocumentFolderService, DocumentFolderService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IForecastDataService, ForecastDataService>();
builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IAccountsPayableService, AccountsPayableService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICountryService, CountryService>();
#endregion

#region Security Services (OWASP Top 10)
// Authorization policies
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddScoped<IAuthorizationHandler, DataOwnershipHandler>();

// Security services
builder.Services.AddScoped<ISecureConfigurationService, SecureConfigurationService>();
builder.Services.AddScoped<IInputValidationService, InputValidationService>();
builder.Services.AddScoped<ISecurityAuditService, SecurityAuditService>();
builder.Services.AddScoped<UrlValidationService>();
builder.Services.AddHttpContextAccessor();

// Session security
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// Configure secure cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
    options.LoginPath = "/api/Authentication/login";
    options.AccessDeniedPath = "/api/login-denied";
});
#endregion

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

#region options pattern

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

#endregion

builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Exception handling with security logging
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Force HTTPS
app.UseHttpsRedirection();
app.UseHsts();

app.UseCors("AngularFrontend");

app.UseSession();

app.UseRouting();

// Request localization
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(locOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }