# Product Requirements Document — Unified Business Management Platform

**Product:** WebApplication2 (CRM/ERP platform)  
**Status:** Living document based on the current application  
**Primary users:** Business administrators, sales teams, operations/procurement teams, finance teams, HR teams, support agents, and marketing teams

## 1. Product summary

The Unified Business Management Platform is a web-based application that brings core CRM and ERP workflows into one system. It gives teams a shared view of customers, sales, inventory, purchasing, finance, people, projects, support, documents, and marketing activity.

The product reduces manual handoffs between disconnected tools and helps managers operate from timely, role-appropriate business data.

## 2. Problem statement

Growing businesses often keep customer information, stock records, purchase orders, employee data, financial transactions, and support work in separate spreadsheets or applications. This creates duplicate work, inconsistent records, delayed reporting, and limited visibility across departments.

The platform must provide an integrated system of record for these workflows while preserving access control, auditability, and support for English and Arabic users.

## 3. Goals

- Centralize day-to-day CRM and ERP workflows in a single browser-based system.
- Enable teams to create, update, search, and review the records needed for their work.
- Connect commercial activity (customers, opportunities, quotes, orders) with operational and financial processes.
- Give administrators control over users, roles, settings, lookup data, and system visibility.
- Support secure authenticated access, localized user interfaces, and traceable system activity.

## 4. Non-goals

- Replacing a specialized enterprise BI, payroll, e-commerce, or warehouse-management system in the initial scope.
- Providing a public API or native mobile client as a core requirement.
- Automatically posting accounting, inventory, or fulfillment transactions without configured business rules and user review.

## 5. Users and permissions

| User | Primary needs |
| --- | --- |
| System administrator | Manage users, roles, settings, lookup data, notifications, and logs. |
| Sales representative/manager | Maintain customers, contacts, leads, opportunities, deals, quotes, activities, and pipelines. |
| Procurement/operations user | Manage suppliers, purchase orders, demand plans, forecasts, shipping, warehouses, and stock movement. |
| Finance user | Manage payables, receivables, journals, ledgers, expenses, banking, reconciliations, and fixed assets. |
| HR user | Manage employees, departments, job titles, hiring, leave, payroll, and locations. |
| Support agent | Manage tickets, service requests, SLAs, support agents, and knowledge content. |
| Marketing user | Create campaigns, templates, segments, automations, social media activity, lead scores, and analytics. |

Access must be authenticated and authorized by role and data-ownership policies. Users must only be able to perform actions permitted by their assigned role and scope.

## 6. Functional requirements

### 6.1 Identity and administration

- Users can register, sign in, and sign out.
- The system validates passwords using strong password rules and locks accounts after repeated failed login attempts.
- Administrators can manage application users and roles, including role assignment.
- Administrators can manage organization settings, business/company records, vendors, countries, states, and other lookup data.
- The system provides notifications and system-log views for authorized users.

### 6.2 CRM and sales

- Users can manage companies, contacts, customers, and sales activities.
- Sales users can manage leads, opportunities, sales pipelines, deals, follow-up tasks, and quotes.
- Users can associate sales records with relevant parties and progress them through their workflow stages.
- Users can view sales-management dashboards and relevant record details.

### 6.3 Inventory and supply chain

- Users can manage products, product types, inventory records, warehouses, and stock transactions.
- Authorized users can record stock in, stock out, and stock adjustments.
- Procurement users can manage suppliers and purchase orders, including purchase-order line items.
- Operations users can maintain demand plans and forecast data.
- Shipping users can calculate shipment information, review options and quotes, create labels, and track shipments where the configured shipping integration supports it.
- The system should provide reorder suggestions/rules and inventory-oriented dashboard views.

### 6.4 Finance and accounting

- Finance users can manage accounts payable invoices and accounts receivable records.
- Users can create and review journal and general-ledger entries.
- Users can manage expenses and expense categories.
- Users can manage bank accounts, bank transactions, reconciliations, and reconciliation items.
- Users can register fixed assets and generate/view depreciation schedules.
- The system supports business documents such as invoices and exports to Excel/PDF where enabled.

### 6.5 Human resources

- HR users can manage employees, departments, job titles, and organization-chart information.
- HR users can manage job postings, applications, and interview scheduling.
- HR users can manage leave types and leave requests.
- HR users can manage payroll records and employee locations.

### 6.6 Customer support and service

- Support users can create, update, view, and manage support tickets and service requests.
- Administrators can manage support-agent assignments and service-level agreements.
- The platform supports a knowledge-base service for customer-service workflows.

### 6.7 Marketing automation

- Marketing users can manage campaigns, campaign types, templates, segments, and automation workflows/steps.
- Users can maintain lead scores and calculate or review campaign analytics/ROI.
- The system supports social-media integration records, scheduled activity, and Facebook/Instagram webhook endpoints.

### 6.8 Projects and documents

- Users can manage projects, job phases, project-related expenses, time entries, and follow-up tasks.
- Users can create folders, document categories, and documents; documents can be stored through the configured file-storage service.

## 7. Cross-functional requirements

### Localization and accessibility

- The user interface must support English and Arabic, with the active culture selected by cookie, query string, or browser language preference.
- Core workflows must use clear validation feedback and keyboard-accessible controls.

### Security

- Authentication is provided through ASP.NET Core Identity.
- Passwords must require at least 12 characters with upper- and lower-case letters, a digit, and a non-alphanumeric character.
- The application must use anti-forgery validation for state-changing browser forms.
- Session and authentication cookies must be HTTP-only, secure, and use strict same-site behavior.
- Input validation, audit/security services, ownership-based authorization, and security headers must be applied consistently.

### Reliability and observability

- The application must persist business data to SQL Server through Entity Framework Core.
- Errors must be handled consistently through global middleware and must not expose sensitive implementation details to users.
- Authorized administrators must be able to review relevant system logs.
- Database health checks should be available to operations teams in production.

### Performance

- List and dashboard views should return usable results within 2 seconds for normal business workloads.
- Frequently used lookup data should be cached where it is safe to do so.
- List-heavy modules should support pagination, filtering, and search as their data volumes grow.

## 8. Success metrics

- ≥90% of active business users can complete their primary workflow without leaving the platform.
- ≥95% of normal authenticated page requests complete within 2 seconds.
- 100% of state-changing requests are traceable to an authenticated user or system process.
- Fewer duplicate customer, product, supplier, and employee records over time.
- Administrators can provision a new user with the appropriate role in under 5 minutes.

## 9. Dependencies and constraints

- ASP.NET Core MVC on .NET 10.
- SQL Server and Entity Framework Core.
- ASP.NET Core Identity and configured authorization policies.
- AutoMapper for DTO/view-model mapping.
- EPPlus for Excel operations and QuestPDF for PDF generation.
- Email and shipping behavior depends on configured external services and credentials.

## 10. Delivery priorities

| Priority | Scope |
| --- | --- |
| P0 | Authentication, role authorization, administration, core CRM, products/inventory, suppliers/purchase orders, core finance records, auditing/logging, localization. |
| P1 | Demand planning, shipping, HR workflows, customer support, documents, projects, notifications, exports. |
| P2 | Marketing automation, social integrations, ROI analytics, advanced dashboards, deeper integrations, mobile/API clients. |

## 11. Open decisions

- Which role matrix and data-ownership rules apply to each module and action?
- Which external email, shipping, and social-media providers are supported in production?
- Which financial postings and inventory movements must be automated versus approved manually?
- What retention, backup, and audit-log retention policies apply to customer, employee, and financial data?
- What reporting KPIs should each dashboard expose, and who owns their definitions?
