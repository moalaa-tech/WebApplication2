using CRM.Domain.Entities;
using CRM.Domain.Entities.HR;
using CRM.Domain.Entities.DocumentManagement;
using CRM.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Entities.InventoryManagement;

namespace CRM.WebApp.DbContext
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            // Ensure database is created
           // context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //context.Database.Migrate();

            // Seed data in order of dependencies
            //SeedRoles(context);
            //SeedUsers(context);
            //SeedCountries(context);
            //SeedStates(context);
            //SeedCities(context);
            //SeedCurrency(context);
            //SeedSuppliers(context);
            //SeedHRData(context);
            //SeedCompanies(context);
            //SeedContacts(context);
            //SeedDocumentCategories(context);
            //SeedDocumentFolders(context);
            //SeedDocuments(context);
            //SeedWarehouses(context);
            //SeedPurchases(context);
            //SeedMeetings(context);
        }

        private static void SeedCompanies(ApplicationContext context)
        {
            var losAngelesId = context.Cities.Single(c => c.Name == "Los Angeles").Id;
            var newYorkCityId = context.Cities.Single(c => c.Name == "New York City").Id;
            var dubaiCityId = context.Cities.Single(c => c.Name == "Dubai City").Id;
            var riyadhCityId = context.Cities.Single(c => c.Name == "Riyadh City").Id;

            var companies = new List<Company>
            {
                new Company {
                    Name = "Tech Solutions Inc.",
                    NameAr = "تك سوليوشنز",
                    Address = "123 Tech Blvd",
                    CityId = losAngelesId,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Company {
                    Name = "Global Innovations",
                    NameAr = "الابتكارات العالمية",
                    Address = "456 Innovation Ave",

                    CityId = newYorkCityId,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Company {
                    Name = "Desert Technologies",
                    NameAr = "تقنيات الصحراء",
                    Address = "789 Desert Road",

                    CityId = riyadhCityId,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Company {
                    Name = "Gulf Enterprises",
                    NameAr = "مؤسسات الخليج",
                    Address = "321 Gulf Street",

                    CityId = dubaiCityId,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Companies.AddRange(companies);
            context.SaveChanges();
        }

        private static void SeedContacts(ApplicationContext context)
        {
            var techSolutionsId = context.Companies.Single(c => c.Name == "Tech Solutions Inc.").Id;
            var globalInnovationsId = context.Companies.Single(c => c.Name == "Global Innovations").Id;
            var desertTechnologiesId = context.Companies.Single(c => c.Name == "Desert Technologies").Id;
            var gulfEnterprisesId = context.Companies.Single(c => c.Name == "Gulf Enterprises").Id;

            var contacts = new List<Contact>
            {
                new Contact {
                    Name = "John",
                    NameAr = "جون",
                    Surname = "Smith",
                    Phone = "555-111-2222",
                    Email = "john.smith@techsolutions.com",
                    Position = "CEO",
                    CompanyId = techSolutionsId,
                    UserId = 1,
                    LastContactDate = DateTime.Now.AddDays(-5),
                    Status = ContactStatus.Active,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                },
                new Contact {
                    Name = "Sarah",
                    NameAr = "سارة",
                    Surname = "Johnson",
                    Phone = "555-222-3333",
                    Email = "sarah.johnson@globalinnovations.com",
                    Position = "Marketing Director",
                    CompanyId = globalInnovationsId,
                    UserId = 1,
                    LastContactDate = DateTime.Now.AddDays(-2),
                    Status = ContactStatus.Active,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Contact {
                    Name = "Mohammed",
                    NameAr = "محمد",
                    Surname = "Al-Farsi",
                    Phone = "555-333-4444",
                    Email = "mohammed.alfarsi@deserttech.com",
                    Position = "CTO",
                    CompanyId = desertTechnologiesId,
                    UserId = 1,
                    LastContactDate = DateTime.Now.AddDays(-10),
                    Status = ContactStatus.Active,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Contact {
                    Name = "Fatima",
                    NameAr = "فاطمة",
                    Surname = "Al-Zahra",
                    Phone = "555-444-5555",
                    Email = "fatima.alzahra@gulfenterprises.com",
                    Position = "Operations Manager",
                    CompanyId = gulfEnterprisesId,
                    UserId = 1,
                    LastContactDate = DateTime.Now.AddDays(-1),
                    Status = ContactStatus.Active,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }

        private static void SeedDocumentCategories(ApplicationContext context)
        {
            var categories = new List<DocumentCategory>
            {
                new DocumentCategory {
                    Name = "Contracts",
                    NameAr = "العقود",
                    Description = "Legal contracts and agreements",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentCategory {
                    Name = "Invoices",
                    NameAr = "الفواتير",
                    Description = "Customer and vendor invoices",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentCategory {
                    Name = "Reports",
                    NameAr = "التقارير",
                    Description = "Business reports and analytics",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentCategory {
                    Name = "Presentations",
                    NameAr = "العروض التقديمية",
                    Description = "Sales and marketing presentations",
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.DocumentCategories.AddRange(categories);
            context.SaveChanges();
        }

        private static void SeedWarehouses(ApplicationContext context)
        {
            var losAngelesId = context.Cities.Single(c => c.Name == "Los Angeles").Id;
            var newYorkCityId = context.Cities.Single(c => c.Name == "New York City").Id;
            var riyadhCityId = context.Cities.Single(c => c.Name == "Riyadh City").Id;

            var warehouses = new List<Warehouse>
            {
                new Warehouse {
                    Name = "Main Warehouse",
                    NameAr = "المستودع الرئيسي",
                    Location = "123 Storage Ave, Los Angeles",
                    CityId = losAngelesId,
                    City = null!,
                    WarehouseTypeId = 0,
                    WarehouseType = null!,
                    Capacity = 10000,
                    ManagerId = 1,
                    Manager = null!,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Warehouse {
                    Name = "East Coast Distribution Center",
                    NameAr = "مركز توزيع الساحل الشرقي",
                    Location = "456 Logistics Blvd, New York",
                    CityId = newYorkCityId,
                    City = null!,
                    WarehouseTypeId = 0,
                    WarehouseType = null!,
                    Capacity = 15000,
                    ManagerId = 1,
                    Manager = null!,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Warehouse {
                    Name = "Middle East Hub",
                    NameAr = "مركز الشرق الأوسط",
                    Location = "789 Storage St, Riyadh",
                    CityId = riyadhCityId,
                    City = null!,
                    WarehouseTypeId = 0,
                    WarehouseType = null!,
                    Capacity = 8000,
                    ManagerId = 1,
                    Manager = null!,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Warehouses.AddRange(warehouses);
            context.SaveChanges();
        }

        private static void SeedPurchases(ApplicationContext context)
        {
            var supplier1Id = context.Suppliers.First(s => s.Name == "Supplier 1").Id;
            var supplier2Id = context.Suppliers.First(s => s.Name == "Supplier 2").Id;
            var supplier3Id = context.Suppliers.First(s => s.Name == "Supplier 3").Id;

            var purchases = new List<PurchaseOrder>
            {
                new PurchaseOrder {
                    OrderNumber = "PO-2023-001",
                    OrderDate = DateTime.Now.AddDays(-30),
                    DeliveryDate = DateTime.Now.AddDays(-20),
                    SupplierId = supplier1Id,
                    Status = "Completed",
                    TotalAmount = 5000.00m,
                    PaymentTerms = "Net 30",
                    Notes = "Regular monthly order",
                    DateCreated = DateTime.Now.AddDays(-30),
                    IsActive = true
                },
                new PurchaseOrder {
                    OrderNumber = "PO-2023-002",
                    OrderDate = DateTime.Now.AddDays(-15),
                    DeliveryDate = DateTime.Now.AddDays(-5),
                    SupplierId = supplier2Id,
                    Status = "Completed",
                    TotalAmount = 7500.00m,
                    PaymentTerms = "Net 30",
                    Notes = "Urgent order for project requirements",
                    DateCreated = DateTime.Now.AddDays(-15),
                    IsActive = true
                },
                new PurchaseOrder {
                    OrderNumber = "PO-2023-003",
                    OrderDate = DateTime.Now.AddDays(-5),
                    DeliveryDate = DateTime.Now.AddDays(5),
                    SupplierId = supplier3Id,
                    Status = "In Progress",
                    TotalAmount = 3200.00m,
                    PaymentTerms = "Net 15",
                    Notes = "Quarterly supply order",
                    DateCreated = DateTime.Now.AddDays(-5),
                    IsActive = true
                }
            };

            context.PurchaseOrders.AddRange(purchases);
            context.SaveChanges();
        }

        private static void SeedMeetings(ApplicationContext context)
        {
            // Create meetings
            var meetings = new List<Meeting>
            {
                new Meeting {
                    Name = "Quarterly Review",
                    NameAr = "المراجعة الربعية",
                    Location = "Conference Room A",
                    Description = "Review of Q3 performance and Q4 planning",
                    AllDay = false,
                    From = DateTime.Now.AddDays(5).AddHours(10),
                    To = DateTime.Now.AddDays(5).AddHours(12),
                    Host = "John Smith",
                    IsHasParticipants = true,
                    RelatedTo = 1, // Assuming this relates to a specific entity with ID 1
                    IsRepeated = true,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Meeting {
                    Name = "Project Kickoff",
                    NameAr = "بدء المشروع",
                    Location = "Meeting Room B",
                    Description = "Initial kickoff for the new client project",
                    AllDay = false,
                    From = DateTime.Now.AddDays(2).AddHours(14),
                    To = DateTime.Now.AddDays(2).AddHours(15),
                    Host = "Sarah Johnson",
                    IsHasParticipants = true,
                    RelatedTo = 2, // Assuming this relates to a specific entity with ID 2
                    IsRepeated = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Meeting {
                    Name = "Annual Planning",
                    NameAr = "التخطيط السنوي",
                    Location = "Executive Boardroom",
                    Description = "Annual strategic planning session",
                    AllDay = true,
                    From = DateTime.Now.AddDays(15),
                    To = DateTime.Now.AddDays(15).AddHours(23).AddMinutes(59),
                    Host = "Mohammed Al-Farsi",
                    IsHasParticipants = true,
                    RelatedTo = 3, // Assuming this relates to a specific entity with ID 3
                    IsRepeated = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Meetings.AddRange(meetings);
            context.SaveChanges();

            // Create participants (assuming Participant entity exists)
            // This part would need to be implemented if you have a Participant entity

            // Create reminders
            var quarterlyReviewId = context.Meetings.Single(m => m.Name == "Quarterly Review").Id;
            var projectKickoffId = context.Meetings.Single(m => m.Name == "Project Kickoff").Id;
            var annualPlanningId = context.Meetings.Single(m => m.Name == "Annual Planning").Id;

            var reminders = new List<Reminder>
            {
                new Reminder {
                    MeetingId = quarterlyReviewId,
                    IsBefore = true,
                    IsAfter = false,
                    Minute = 30,
                    Hour = 0,
                    Day = 0,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Reminder {
                    MeetingId = projectKickoffId,
                    IsBefore = true,
                    IsAfter = false,
                    Minute = 15,
                    Hour = 0,
                    Day = 0,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Reminder {
                    MeetingId = annualPlanningId,
                    IsBefore = true,
                    IsAfter = false,
                    Minute = 0,
                    Hour = 1,
                    Day = 0,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Reminders.AddRange(reminders);
            context.SaveChanges();

            // Create repeated meeting
            var quarterlyReview = context.Meetings.Single(m => m.Name == "Quarterly Review");

            var meetingRepeated = new MeetingRepeated
            {
                MeetingId = quarterlyReview.Id,
                AllDay = false,
                RepeatedType = "Monthly",
                From = quarterlyReview.From,
                To = quarterlyReview.To,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            context.MeetingRepeateds.AddRange(meetingRepeated);
            context.SaveChanges();
        }

        private static void SeedRoles(ApplicationContext context)
        {
            if (context.Roles.Any())
                return;

            var roles = new List<CRM.Domain.IdentityEntity.ApplicationRole>
            {
                new CRM.Domain.IdentityEntity.ApplicationRole { Name = "Admin", NormalizedName = "ADMIN" , NameAR="ادارى" },
                new CRM.Domain.IdentityEntity.ApplicationRole { Name = "Manager", NormalizedName = "MANAGER" ,NameAR="مدير" },
                new CRM.Domain.IdentityEntity.ApplicationRole { Name = "Employee", NormalizedName = "EMPLOYEE" , NameAR="موظف"},
                new CRM.Domain.IdentityEntity.ApplicationRole { Name = "SalesRep", NormalizedName = "SALESREP", NameAR="موظف مبيعات" },
                new CRM.Domain.IdentityEntity.ApplicationRole { Name = "HRManager", NormalizedName = "HRMANAGER" , NameAR="مدير موارد بشريه"}
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private static void SeedUsers(ApplicationContext context)
        {
            if (context.Users.Any())
                return;

            var users = new List<CRM.Domain.IdentityEntity.ApplicationUser>
            {
                new CRM.Domain.IdentityEntity.ApplicationUser {
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin@crm.com",
                    NormalizedUserName = "ADMIN@CRM.COM",
                    Email = "admin@crm.com",
                    NormalizedEmail = "ADMIN@CRM.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEOzeajp5etEMZKoLqGVmRAoHf1AUj5vMbmMEdrCHQaCBXEq7qOg6+aUE8RPTLWqAWQ==", // Password123!
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NameAR = "المستخدم الادارى"
                },
                new CRM.Domain.IdentityEntity.ApplicationUser {
                    FirstName = "John",
                    LastName = "Manager",
                    UserName = "john.manager@crm.com",
                    NormalizedUserName = "JOHN.MANAGER@CRM.COM",
                    Email = "john.manager@crm.com",
                    NormalizedEmail = "JOHN.MANAGER@CRM.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEOzeajp5etEMZKoLqGVmRAoHf1AUj5vMbmMEdrCHQaCBXEq7qOg6+aUE8RPTLWqAWQ==", // Password123!
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NameAR = "مدير جون"
                },
                new CRM.Domain.IdentityEntity.ApplicationUser {
                    FirstName = "Sarah",
                    LastName = "Sales",
                    UserName = "sarah.sales@crm.com",
                    NormalizedUserName = "SARAH.SALES@CRM.COM",
                    Email = "sarah.sales@crm.com",
                    NormalizedEmail = "SARAH.SALES@CRM.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEOzeajp5etEMZKoLqGVmRAoHf1AUj5vMbmMEdrCHQaCBXEq7qOg6+aUE8RPTLWqAWQ==", // Password123!
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NameAR = "مندوبة المبيعات سارة"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void SeedCountries(ApplicationContext context)
        {
            if (context.Countries.Any())
                return;

            var countries = new List<Country>
            {
                new Country {
                    Name = "Egypt",
                    NameAr = "مصر",
                    ISO2 = "EG",
                    ISO3 = "EG",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Country {
                    Name = "Saudi Arabia",
                    NameAr = "المملكة العربية السعودية",
                    ISO2 = "SA",
                    ISO3 = "SAU",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Country {
                    Name = "United Arab Emirates",
                    NameAr = "دولة الإمارات العربية المتحدة",
                    ISO2 = "AE",
                    ISO3 = "ARE",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Country {
                    Name = "United Kingdom",
                    NameAr = "المملكة المتحدة",
                    ISO2 = "GB",
                    ISO3 = "GBR",
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Countries.AddRange(countries);
            context.SaveChanges();
        }

        private static void SeedStates(ApplicationContext context)
        {
            if (context.States.Any())
                return;
           
            var egyptCountryId = 1;
            var states = new List<State>
           {
            new() { Name = "Cairo", NameAr = "القاهرة", CountryId = egyptCountryId },
            new() { Name = "Giza", NameAr = "الجيزة", CountryId = egyptCountryId },
            new() { Name = "Alexandria", NameAr = "الإسكندرية", CountryId = egyptCountryId },
            new() { Name = "Port Said", NameAr = "بورسعيد", CountryId = egyptCountryId },
            new() { Name = "Suez", NameAr = "السويس", CountryId = egyptCountryId },
            new() { Name = "Dakahlia", NameAr = "الدقهلية", CountryId = egyptCountryId },
            new() { Name = "Sharqia", NameAr = "الشرقية", CountryId = egyptCountryId },
            new() { Name = "Qalyubia", NameAr = "القليوبية", CountryId = egyptCountryId },
            new() { Name = "Kafr El Sheikh", NameAr = "كفر الشيخ", CountryId = egyptCountryId },
            new() { Name = "Gharbia", NameAr = "الغربية", CountryId = egyptCountryId },
            new() { Name = "Monufia", NameAr = "المنوفية", CountryId = egyptCountryId },
            new() { Name = "Beheira", NameAr = "البحيرة", CountryId = egyptCountryId },
            new() { Name = "Ismailia", NameAr = "الإسماعيلية", CountryId = egyptCountryId },
            new() { Name = "Minya", NameAr = "المنيا", CountryId = egyptCountryId },
            new() { Name = "Beni Suef", NameAr = "بني سويف", CountryId = egyptCountryId },
            new() { Name = "Faiyum", NameAr = "الفيوم", CountryId = egyptCountryId },
            new() { Name = "Assiut", NameAr = "أسيوط", CountryId = egyptCountryId },
            new() { Name = "Sohag", NameAr = "سوهاج", CountryId = egyptCountryId },
            new() { Name = "Qena", NameAr = "قنا", CountryId = egyptCountryId },
            new() { Name = "Aswan", NameAr = "أسوان", CountryId = egyptCountryId },
            new() { Name = "Luxor", NameAr = "الأقصر", CountryId = egyptCountryId },
            new() { Name = "Red Sea", NameAr = "البحر الأحمر", CountryId = egyptCountryId },
            new() { Name = "New Valley", NameAr = "الوادي الجديد", CountryId = egyptCountryId },
            new() { Name = "Matrouh", NameAr = "مطروح", CountryId = egyptCountryId },
            new() { Name = "North Sinai", NameAr = "شمال سيناء", CountryId = egyptCountryId },
            new() { Name = "South Sinai", NameAr = "جنوب سيناء", CountryId = egyptCountryId }
        };

            context.States.AddRange(states);
            context.SaveChanges();
        }

        private static void SeedCities(ApplicationContext context)
        {
            if (context.Cities.Any())
                return;

            var cairoStateId = 1;

            var cities = new List<City>
        {
            new() { Name = "Heliopolis", NameAr = "مصر الجديدة", StateId = cairoStateId },
            new() { Name = "Nasr City", NameAr = "مدينة نصر", StateId = cairoStateId },
            new() { Name = "Maadi", NameAr = "المعادي", StateId = cairoStateId },
            new() { Name = "Zamalek", NameAr = "الزمالك", StateId = cairoStateId },
            new() { Name = "Downtown", NameAr = "وسط البلد", StateId = cairoStateId },
            new() { Name = "Shubra", NameAr = "شبرا", StateId = cairoStateId },
            new() { Name = "Ain Shams", NameAr = "عين شمس", StateId = cairoStateId },
            new() { Name = "Helwan", NameAr = "حلوان", StateId = cairoStateId },
            new() { Name = "El Marg", NameAr = "المرج", StateId = cairoStateId },
            new() { Name = "El Matareya", NameAr = "المطرية", StateId = cairoStateId },
            new() { Name = "El Salam City", NameAr = "مدينة السلام", StateId = cairoStateId },
            new() { Name = "El Basatin", NameAr = "البساتين", StateId = cairoStateId },
            new() { Name = "El Mokattam", NameAr = "المقطم", StateId = cairoStateId },
            new() { Name = "El Sayeda Zeinab", NameAr = "السيدة زينب", StateId = cairoStateId },
            new() { Name = "El Manial", NameAr = "المنيل", StateId = cairoStateId },
            new() { Name = "El Marg City", NameAr = "مدينة المرج", StateId = cairoStateId },
            new() { Name = "Old Cairo", NameAr = "مصر القديمة", StateId = cairoStateId },
            new() { Name = "El Shorouk", NameAr = "الشروق", StateId = cairoStateId },
            new() { Name = "New Cairo", NameAr = "القاهرة الجديدة", StateId = cairoStateId },
            new() { Name = "Badr City", NameAr = "مدينة بدر", StateId = cairoStateId },
            new() { Name = "15th of May City", NameAr = "مدينة 15 مايو", StateId = cairoStateId },
            new() { Name = "Obour City", NameAr = "مدينة العبور", StateId = cairoStateId },
            new() { Name = "El Tebbin", NameAr = "التبين", StateId = cairoStateId },
            new() { Name = "Tura", NameAr = "طرة", StateId = cairoStateId },
            new() { Name = "El Zawya El Hamra", NameAr = "الزاوية الحمراء", StateId = cairoStateId },
            new() { Name = "El Darb El Ahmar", NameAr = "الدرب الأحمر", StateId = cairoStateId },
            new() { Name = "El Khalifa", NameAr = "الخليفة", StateId = cairoStateId },
            new() { Name = "El Gamaliya", NameAr = "الجمالية", StateId = cairoStateId },
            new() { Name = "El Daher", NameAr = "الظاهر", StateId = cairoStateId },
            new() { Name = "Bab El Sharia", NameAr = "باب الشعرية", StateId = cairoStateId }
        };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }

        private static void SeedCurrency(ApplicationContext context)
        {
            if (context.Currencies.Any())
                return;

            var currencies = new List<Currency>
            {
                new Currency {
                    Name = "US Dollar",
                    NameAr = "الدولار الأمريكي",
                    Code = "USD",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Currency {
                    Name = "Saudi Riyal",
                    NameAr = "الريال السعودي",
                    Code = "SAR",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Currency {
                    Name = "UAE Dirham",
                    NameAr = "الدرهم الإماراتي",
                    Code = "AED",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Currency {
                    Name = "Euro",
                    NameAr = "اليورو",
                    Code = "EUR",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Currency {
                    Name = "British Pound",
                    NameAr = "الجنيه الإسترليني",
                    Code = "GBP",
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Currencies.AddRange(currencies);
            context.SaveChanges();
        }

        private static void SeedSuppliers(ApplicationContext context)
        {
            if (context.Suppliers.Any())
                return;

            var suppliers = new List<Supplier>
            {
                new Supplier {
                    Name = "Supplier 1",
                    NameAr = "المورد الأول",
                    Code = "SUP001",
                    ContactEmail = "contact@supplier1.com",
                    ContactPhone = "+1-555-0001",
                    Status = CRM.Domain.Enums.SupplyChainManagement.SupplierStatus.Active,
                    ContractStartDate = DateTime.Now.AddMonths(-12),
                    ContractEndDate = DateTime.Now.AddMonths(12),
                    Address = "123 Supplier Street, Los Angeles, CA",
                    ContactPerson = "John Supplier",
                    PhoneNumber = "+1-555-0001",
                    Email = "contact@supplier1.com",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Supplier {
                    Name = "Supplier 2",
                    NameAr = "المورد الثاني",
                    Code = "SUP002",
                    ContactEmail = "info@supplier2.com",
                    ContactPhone = "+1-555-0002",
                    Status = CRM.Domain.Enums.SupplyChainManagement.SupplierStatus.Active,
                    ContractStartDate = DateTime.Now.AddMonths(-6),
                    ContractEndDate = DateTime.Now.AddMonths(18),
                    Address = "456 Vendor Avenue, New York, NY",
                    ContactPerson = "Sarah Vendor",
                    PhoneNumber = "+1-555-0002",
                    Email = "info@supplier2.com",
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Supplier {
                    Name = "Supplier 3",
                    NameAr = "المورد الثالث",
                    Code = "SUP003",
                    ContactEmail = "sales@supplier3.com",
                    ContactPhone = "+966-11-555-0003",
                    Status = CRM.Domain.Enums.SupplyChainManagement.SupplierStatus.Active,
                    ContractStartDate = DateTime.Now.AddMonths(-3),
                    ContractEndDate = DateTime.Now.AddMonths(21),
                    Address = "789 Supply Road, Riyadh, KSA",
                    ContactPerson = "Mohammed Al-Supplier",
                    PhoneNumber = "+966-11-555-0003",
                    Email = "sales@supplier3.com",
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void SeedHRData(ApplicationContext context)
        {
            if (context.Departments.Any())
                return;

            // Seed Departments first
            var departments = new List<Department>
            {
                new Department {
                    Name = "Human Resources",
                    NameAr = "الموارد البشرية",
                    Description = "Manages employee relations and company policies",
                    Location = "Building A, Floor 2",
                    Budget = 250000m,
                    EstablishedDate = DateTime.Now.AddYears(-5),
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Department {
                    Name = "Sales",
                    NameAr = "المبيعات",
                    Description = "Handles customer acquisition and revenue generation",
                    Location = "Building B, Floor 1",
                    Budget = 500000m,
                    EstablishedDate = DateTime.Now.AddYears(-10),
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Department {
                    Name = "IT Support",
                    NameAr = "دعم تقنية المعلومات",
                    Description = "Provides technical support and infrastructure management",
                    Location = "Building C, Floor 3",
                    Budget = 300000m,
                    EstablishedDate = DateTime.Now.AddYears(-7),
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new Department {
                    Name = "Finance",
                    NameAr = "المالية",
                    Description = "Manages company finances and accounting",
                    Location = "Building A, Floor 3",
                    Budget = 200000m,
                    EstablishedDate = DateTime.Now.AddYears(-8),
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            // Seed Job Titles
            if (!context.JobTitles.Any())
            {
                var jobTitles = new List<JobTitle>
                {
                    new JobTitle {
                        Title = "Software Developer",
                        TitleAr = "مطور برمجيات",
                        DepartmentId = 1, // IT Department
                        DateCreated = DateTime.Now,
                        IsActive = true
                    },
                    new JobTitle {
                        Title = "Sales Manager",
                        TitleAr = "مدير مبيعات",
                        DepartmentId = 2, // Sales Department
                        DateCreated = DateTime.Now,
                        IsActive = true
                    },
                    new JobTitle {
                        Title = "HR Specialist",
                        TitleAr = "أخصائي موارد بشرية",
                        DepartmentId = 1, // HR Department
                        DateCreated = DateTime.Now,
                        IsActive = true
                    },
                    new JobTitle {
                        Title = "Financial Analyst",
                        TitleAr = "محلل مالي",
                        DepartmentId = 4, // Finance Department
                        DateCreated = DateTime.Now,
                        IsActive = true
                    }
                };

                context.JobTitles.AddRange(jobTitles);
                context.SaveChanges();
            }

            // Seed Leave Types
            if (!context.LeaveTypes.Any())
            {
                var leaveTypes = new List<LeaveType>
                {
                    new LeaveType {
                        Name = "Annual Leave",
                        NameAr = "الإجازة السنوية",
                        DateCreated = DateTime.Now,
                        IsActive = true
                    },
                    new LeaveType {
                        Name = "Sick Leave",
                        NameAr = "إجازة مرضية",
                        DateCreated = DateTime.Now,
                        IsActive = true
                    },
                    new LeaveType {
                        Name = "Maternity Leave",
                        NameAr = "إجازة أمومة",
                        DateCreated = DateTime.Now,
                        IsActive = true
                    },
                    new LeaveType {
                        Name = "Emergency Leave",
                        NameAr = "إجازة طوارئ",
                        DateCreated = DateTime.Now,
                        IsActive = true
                    }
                };

                context.LeaveTypes.AddRange(leaveTypes);
                context.SaveChanges();
            }
        }

        private static void SeedDocumentFolders(ApplicationContext context)
        {
            if (context.DocumentFolders.Any())
                return;

            var folders = new List<DocumentFolder>
            {
                new DocumentFolder {
                    Name = "Root",
                    Description = "Root folder for all documents",
                    Path = "/",
                    IsSystemFolder = true,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentFolder {
                    Name = "Contracts",
                    Description = "All contract documents",
                    Path = "/Contracts",
                    IsSystemFolder = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentFolder {
                    Name = "HR Documents",
                    Description = "Human Resources related documents",
                    Path = "/HR",
                    IsSystemFolder = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentFolder {
                    Name = "Financial Reports",
                    Description = "Financial and accounting documents",
                    Path = "/Finance",
                    IsSystemFolder = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                },
                new DocumentFolder {
                    Name = "Marketing Materials",
                    Description = "Marketing and promotional documents",
                    Path = "/Marketing",
                    IsSystemFolder = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }
            };

            context.DocumentFolders.AddRange(folders);
            context.SaveChanges();

            // Set parent folder relationships
            var rootFolder = context.DocumentFolders.Single(f => f.Name == "Root");
            var subFolders = context.DocumentFolders.Where(f => f.Name != "Root").ToList();

            foreach (var folder in subFolders)
            {
                folder.ParentFolderId = rootFolder.Id;
            }

            context.SaveChanges();
        }

        private static void SeedDocuments(ApplicationContext context)
        {
            if (context.Documents.Any())
                return;

            var contractsCategory = context.DocumentCategories.Single(c => c.Name == "Contracts");
            var invoicesCategory = context.DocumentCategories.Single(c => c.Name == "Invoices");
            var reportsCategory = context.DocumentCategories.Single(c => c.Name == "Reports");

            var contractsFolder = context.DocumentFolders.Single(f => f.Name == "Contracts");
            var financeFolder = context.DocumentFolders.Single(f => f.Name == "Financial Reports");
            var hrFolder = context.DocumentFolders.Single(f => f.Name == "HR Documents");

            var documents = new List<Document>
            {
                new Document {
                    Title = "Employee Handbook 2024",
                    Description = "Complete employee handbook with policies and procedures",
                    DocumentType = "Policy",
                    FileName = "employee_handbook_2024.pdf",
                    FileExtension = ".pdf",
                    FilePath = "/documents/hr/employee_handbook_2024.pdf",
                    FileSize = 2048576, // 2MB
                    MimeType = "application/pdf",
                    Version = 1,
                    CategoryId = reportsCategory.Id,
                    FolderId = hrFolder.Id,
                    UploadedBy = "admin@crm.com",
                    UploadDate = DateTime.Now.AddDays(-30),
                    IsPublic = false,
                    DateCreated = DateTime.Now.AddDays(-30),
                    IsActive = true
                },
                new Document {
                    Title = "Service Agreement Template",
                    Description = "Standard service agreement template for clients",
                    DocumentType = "Template",
                    FileName = "service_agreement_template.docx",
                    FileExtension = ".docx",
                    FilePath = "/documents/contracts/service_agreement_template.docx",
                    FileSize = 512000, // 500KB
                    MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Version = 2,
                    CategoryId = contractsCategory.Id,
                    FolderId = contractsFolder.Id,
                    UploadedBy = "admin@crm.com",
                    UploadDate = DateTime.Now.AddDays(-45),
                    IsPublic = false,
                    DateCreated = DateTime.Now.AddDays(-45),
                    IsActive = true
                },
                new Document {
                    Title = "Q3 Financial Report",
                    Description = "Quarterly financial performance report",
                    DocumentType = "Report",
                    FileName = "q3_financial_report.xlsx",
                    FileExtension = ".xlsx",
                    FilePath = "/documents/finance/q3_financial_report.xlsx",
                    FileSize = 1536000, // 1.5MB
                    MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Version = 1,
                    CategoryId = reportsCategory.Id,
                    FolderId = financeFolder.Id,
                    UploadedBy = "john.manager@crm.com",
                    UploadDate = DateTime.Now.AddDays(-15),
                    IsPublic = false,
                    DateCreated = DateTime.Now.AddDays(-15),
                    IsActive = true
                },
                new Document {
                    Title = "Master Services Agreement - TechCorp",
                    Description = "Signed master services agreement with TechCorp",
                    DocumentType = "Contract",
                    FileName = "msa_techcorp_2024.pdf",
                    FileExtension = ".pdf",
                    FilePath = "/documents/contracts/msa_techcorp_2024.pdf",
                    FileSize = 3072000, // 3MB
                    MimeType = "application/pdf",
                    Version = 1,
                    CategoryId = contractsCategory.Id,
                    FolderId = contractsFolder.Id,
                    UploadedBy = "sarah.sales@crm.com",
                    UploadDate = DateTime.Now.AddDays(-7),
                    IsPublic = false,
                    DateCreated = DateTime.Now.AddDays(-7),
                    IsActive = true
                }
            };

            context.Documents.AddRange(documents);
            context.SaveChanges();
        }
    }
}
