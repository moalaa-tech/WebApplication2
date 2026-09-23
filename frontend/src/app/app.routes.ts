import { Routes } from '@angular/router';
import { Dashboard } from './features/dashboard/dashboard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', component: Dashboard },

  { path: 'activities', loadComponent: () => import('./features/activities/activities').then((m) => m.ActivitiesComponent) },
  { path: 'authentication', loadComponent: () => import('./features/authentication/authentication').then((m) => m.AuthenticationComponent) },
  { path: 'companies', loadComponent: () => import('./features/companies/companies').then((m) => m.CompaniesComponent) },
  { path: 'company', loadComponent: () => import('./features/company/company').then((m) => m.CompanyComponent) },
  { path: 'contact', loadComponent: () => import('./features/contact/contact').then((m) => m.ContactComponent) },
  { path: 'health', loadComponent: () => import('./features/health/health').then((m) => m.HealthComponent) },
  { path: 'home', loadComponent: () => import('./features/home/home').then((m) => m.HomeComponent) },
  { path: 'lookup', loadComponent: () => import('./features/lookup/lookup').then((m) => m.LookupComponent) },
  { path: 'notifications', loadComponent: () => import('./features/notifications/notifications').then((m) => m.NotificationsComponent) },
  { path: 'role', loadComponent: () => import('./features/role/role').then((m) => m.RoleComponent) },
  { path: 'setting', loadComponent: () => import('./features/setting/setting').then((m) => m.SettingComponent) },
  { path: 'system-logs', loadComponent: () => import('./features/system-logs/system-logs').then((m) => m.SystemLogsComponent) },
  { path: 'user', loadComponent: () => import('./features/user/user').then((m) => m.UserComponent) },
  { path: 'vendors', loadComponent: () => import('./features/vendors/vendors').then((m) => m.VendorsComponent) },

  { path: 'accounts-payable', loadComponent: () => import('./features/accounts-payable/accounts-payable').then((m) => m.AccountsPayableComponent) },
  { path: 'accounts-receivable', loadComponent: () => import('./features/accounts-receivable/accounts-receivable').then((m) => m.AccountsReceivableComponent) },
  { path: 'expense-categories', loadComponent: () => import('./features/expense-categories/expense-categories').then((m) => m.ExpenseCategoriesComponent) },
  { path: 'expenses', loadComponent: () => import('./features/expenses/expenses').then((m) => m.ExpensesComponent) },
  { path: 'general-ledger', loadComponent: () => import('./features/general-ledger/general-ledger').then((m) => m.GeneralLedgerComponent) },
  { path: 'journal', loadComponent: () => import('./features/journal/journal').then((m) => m.JournalComponent) },

  { path: 'fixed-assets', loadComponent: () => import('./features/fixed-assets/fixed-assets').then((m) => m.FixedAssetsComponent) },

  { path: 'bank-accounts', loadComponent: () => import('./features/bank-accounts/bank-accounts').then((m) => m.BankAccountsComponent) },
  { path: 'bank-transactions', loadComponent: () => import('./features/bank-transactions/bank-transactions').then((m) => m.BankTransactionsComponent) },
  { path: 'reconciliation-items', loadComponent: () => import('./features/reconciliation-items/reconciliation-items').then((m) => m.ReconciliationItemsComponent) },
  { path: 'reconciliations', loadComponent: () => import('./features/reconciliations/reconciliations').then((m) => m.ReconciliationsComponent) },

  { path: 'service-level-agreements', loadComponent: () => import('./features/service-level-agreements/service-level-agreements').then((m) => m.ServiceLevelAgreementsComponent) },
  { path: 'service-request', loadComponent: () => import('./features/service-request/service-request').then((m) => m.ServiceRequestComponent) },
  { path: 'support-agents', loadComponent: () => import('./features/support-agents/support-agents').then((m) => m.SupportAgentsComponent) },
  { path: 'tickets', loadComponent: () => import('./features/tickets/tickets').then((m) => m.TicketsComponent) },

  { path: 'document-categories', loadComponent: () => import('./features/document-categories/document-categories').then((m) => m.DocumentCategoriesComponent) },
  { path: 'document-folders', loadComponent: () => import('./features/document-folders/document-folders').then((m) => m.DocumentFoldersComponent) },
  { path: 'documents', loadComponent: () => import('./features/documents/documents').then((m) => m.DocumentsComponent) },

  { path: 'department', loadComponent: () => import('./features/department/department').then((m) => m.DepartmentComponent) },
  { path: 'employee', loadComponent: () => import('./features/employee/employee').then((m) => m.EmployeeComponent) },
  { path: 'job-applications', loadComponent: () => import('./features/job-applications/job-applications').then((m) => m.JobApplicationsComponent) },
  { path: 'job-postings', loadComponent: () => import('./features/job-postings/job-postings').then((m) => m.JobPostingsComponent) },
  { path: 'job-title', loadComponent: () => import('./features/job-title/job-title').then((m) => m.JobTitleComponent) },
  { path: 'leave-request', loadComponent: () => import('./features/leave-request/leave-request').then((m) => m.LeaveRequestComponent) },
  { path: 'leave-types', loadComponent: () => import('./features/leave-types/leave-types').then((m) => m.LeaveTypesComponent) },
  { path: 'location', loadComponent: () => import('./features/location/location').then((m) => m.LocationComponent) },
  { path: 'payrolls', loadComponent: () => import('./features/payrolls/payrolls').then((m) => m.PayrollsComponent) },

  { path: 'inventory', loadComponent: () => import('./features/inventory/inventory').then((m) => m.InventoryComponent) },
  { path: 'order', loadComponent: () => import('./features/order/order').then((m) => m.OrderComponent) },
  { path: 'product', loadComponent: () => import('./features/product/product').then((m) => m.ProductComponent) },
  { path: 'product-type', loadComponent: () => import('./features/product-type/product-type').then((m) => m.ProductTypeComponent) },
  { path: 'stock', loadComponent: () => import('./features/stock/stock').then((m) => m.StockComponent) },
  { path: 'transactions', loadComponent: () => import('./features/transactions/transactions').then((m) => m.TransactionsComponent) },
  { path: 'warehouse', loadComponent: () => import('./features/warehouse/warehouse').then((m) => m.WarehouseComponent) },

  { path: 'analytics-roi-tracking', loadComponent: () => import('./features/analytics-roi-tracking/analytics-roi-tracking').then((m) => m.AnalyticsROITrackingComponent) },
  { path: 'campaigns', loadComponent: () => import('./features/campaigns/campaigns').then((m) => m.CampaignsComponent) },
  { path: 'email-template', loadComponent: () => import('./features/email-template/email-template').then((m) => m.EmailTemplateComponent) },
  { path: 'facebook-webhook', loadComponent: () => import('./features/facebook-webhook/facebook-webhook').then((m) => m.FacebookWebhookComponent) },
  { path: 'instagram-webhook', loadComponent: () => import('./features/instagram-webhook/instagram-webhook').then((m) => m.InstagramWebhookComponent) },
  { path: 'lead-scoring', loadComponent: () => import('./features/lead-scoring/lead-scoring').then((m) => m.LeadScoringComponent) },
  { path: 'segmentation', loadComponent: () => import('./features/segmentation/segmentation').then((m) => m.SegmentationComponent) },
  { path: 'social-media-integration', loadComponent: () => import('./features/social-media-integration/social-media-integration').then((m) => m.SocialMediaIntegrationComponent) },

  { path: 'follow-up-task', loadComponent: () => import('./features/follow-up-task/follow-up-task').then((m) => m.FollowUpTaskComponent) },
  { path: 'job-phases', loadComponent: () => import('./features/job-phases/job-phases').then((m) => m.JobPhasesComponent) },
  { path: 'projects', loadComponent: () => import('./features/projects/projects').then((m) => m.ProjectsComponent) },
  { path: 'time-entries', loadComponent: () => import('./features/time-entries/time-entries').then((m) => m.TimeEntriesComponent) },

  { path: 'automations', loadComponent: () => import('./features/automations/automations').then((m) => m.AutomationsComponent) },
  { path: 'automation-step', loadComponent: () => import('./features/automation-step/automation-step').then((m) => m.AutomationStepComponent) },
  { path: 'customer', loadComponent: () => import('./features/customer/customer').then((m) => m.CustomerComponent) },
  { path: 'deals', loadComponent: () => import('./features/deals/deals').then((m) => m.DealsComponent) },
  { path: 'quotes', loadComponent: () => import('./features/quotes/quotes').then((m) => m.QuotesComponent) },
  { path: 'sales', loadComponent: () => import('./features/sales/sales').then((m) => m.SalesComponent) },

  { path: 'demand-plans', loadComponent: () => import('./features/demand-plans/demand-plans').then((m) => m.DemandPlansComponent) },
  { path: 'forecast-data', loadComponent: () => import('./features/forecast-data/forecast-data').then((m) => m.ForecastDataComponent) },
  { path: 'purchase-orders', loadComponent: () => import('./features/purchase-orders/purchase-orders').then((m) => m.PurchaseOrdersComponent) },
  { path: 'shipping', loadComponent: () => import('./features/shipping/shipping').then((m) => m.ShippingComponent) },
  { path: 'suppliers', loadComponent: () => import('./features/suppliers/suppliers').then((m) => m.SuppliersComponent) },

  { path: '**', pathMatch: 'full', redirectTo: '' },
];