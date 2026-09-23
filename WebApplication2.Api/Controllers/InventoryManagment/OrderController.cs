using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Entities.HR;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Enums;
using CRM.WebApp.Controllers;
using CRM.WebApp.Controllers.InventoryManagment;
using CRM.WebApp.DbContext;
using CRM.WebApp.DTOs.EasyOrder;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.DTOs.OrderDetails;
using CRM.WebApp.Extensions;
using CRM.WebApp.Helper;
using CRM.WebApp.Paging;
using CRM.WebApp.Services;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.InventoryManagment;
using CRM.WebApp.Services.Lookups;
using CRM.WebApp.Services.HumanResources;
using CRM.WebApp.ViewModels.InventoryManagement.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using QuestPDF.Fluent;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebApplication2.Api.Controllers.InventoryManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService _OrderService;
        private ICustomerService CustomerService;
        private IProductService ProductService;
        private IProductTypeService ProductTypeService;
        private ApplicationContext DbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        private IStateService StateService;
        private ICountryService CountryService;
        private IAuthenticationService AuthenticationService;
        private IEmployeeService EmployeeService;

        public OrderController(

            IAuthenticationService _AuthenticationService,
            ICountryService _CountryService,
            IStateService _StateService,
            ApplicationContext applicationContext,
            ICustomerService _CustomerService,
            IProductTypeService _ProductTypeService,
            IProductService _ProductService,
            IMapper mapper,
            IOrderService _orderService,
            ILogger<OrderController> logger,
            IStringLocalizer<HomeController> localizer,
            IEmployeeService _EmployeeService
        )
        {
            _OrderService = _orderService;
            DbContext = applicationContext;
            _mapper = mapper;
            _logger = logger;
            CustomerService = _CustomerService;
            ProductService = _ProductService;
            ProductTypeService = _ProductTypeService;
            _localizer = localizer;
            StateService = _StateService;
            CountryService = _CountryService;
            AuthenticationService = _AuthenticationService;
            EmployeeService = _EmployeeService;
        }

        [HttpGet("TodayOrders")]
        public async Task<IActionResult> TodayOrders([FromQuery] OrderViewModel model)
        {
            var today = DateTime.Today;
            model.DateFrom = today;
            model.DateTo = today;

            var States = await StateService.GetAllAsync();
            var Cities = await StateService.GetCitiesByStateIdAsync(2);

            //var Contries = await CountryService.GetAllAsync();
            //var Products = await ProductService.GetAllProductsAsync();
            //var Users = await AuthenticationService.GetAllUserAsync();
            //var Customers = await AuthenticationService.GetAllUserAsync();

            var allStates = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.NameAr
            }).ToList();

            var states = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.NameAr,
                Selected = model.StateId == s.Id
            }).ToList();

            var cities = Cities.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.NameAr,
                Selected = model.StateId == s.Id
            }).ToList();

            //var countries = Contries.Select(c => new SelectListItem
            //{
            //    Value = c.Id.ToString(),
            //    Text = c.NameAr,
            //    Selected = model.CountryId == c.Id
            //}).ToList();

            //var products = Products.Select(c => new SelectListItem
            //{
            //    Value = c.Id.ToString(),
            //    Text = c.NameAr,
            //    Selected = model.ProductId == c.Id
            //}).ToList();

            //var customer = Customers.Select(c => new SelectListItem
            //{
            //    Value = c.Id.ToString(),
            //    Text = c.FirstName,
            //    Selected = model.CustomerId == c.Id
            //}).ToList();

            //var users = Users.Select(c => new SelectListItem
            //{
            //    Value = c.Id.ToString(),
            //    Text = c.FirstName ?? c.Email,
            //    Selected = model.EmployeeId == c.Id
            //}).ToList();

            PaginatedList<OrderDto> orders = await _OrderService.GetAllOrdersAsync(model);
            model.Result = orders;
            return Ok(new { model, allStates, states, cities });
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] OrderViewModel model)
        {
            var States = await StateService.GetAllAsync();
            var Contries = await CountryService.GetAllAsync();
            var Products = await ProductService.GetAllProductsAsync();
            var Users = await AuthenticationService.GetAllUserAsync();
            var Customers = await AuthenticationService.GetAllUserAsync();

            var states = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // or s.Id if you want to filter by Id
                Text = s.NameAr,
                Selected = model.StateId == s.Id
            }).ToList();

            var countries = Contries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
                Selected = model.CountryId == c.Id
            }).ToList();

            var products = Products.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
                Selected = model.ProductId == c.Id
            }).ToList();

            var customer = Customers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FirstName,
                Selected = model.CustomerId == c.Id
            }).ToList();

            var users = Users.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FirstName ?? c.Email,
                Selected = model.EmployeeId == c.Id
            }).ToList();

            PaginatedList<OrderDto> orders = await _OrderService.GetAllOrdersAsync(model);
            model.Result = orders;
            return Ok(new { model, states, countries, products, customer, users });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _OrderService.GetOrderByIdAsync(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto createOrderDto)
        {
            if (ModelState.IsValid)
            {
                await _OrderService.CreateOrderAsync(createOrderDto);
                return Ok(createOrderDto);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("AddProduct/{id:int}")]
        public async Task<IActionResult> AddProduct(int id)
        {
            var products = new SelectList(await ProductService.GetAllProductsAsync(), "Id", "Name");

            //var dto = await _OrderService.GetAddProductDtoAsync(id);
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] UpdateOrderDto model)
        {
            if (!ModelState.IsValid)
                return Ok(new { success = false, message = "Invalid data" });

            var result = await _OrderService.UpdateOrderAsync(model);

            if (result == null)
                return Ok(new { success = false, message = "Failed to add product" });

            return Ok(new { success = true });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            if (id != updateOrderDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _OrderService.OrderExistsAsync(id))
            {
                return NotFound();
            }
            var mm = await _OrderService.UpdateOrderAsync(updateOrderDto);
            return Ok(updateOrderDto);
        }

        [HttpGet("EditStatus")]
        public async Task<IActionResult> EditStatus([FromQuery] int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            OrderDto? orderDto = await _OrderService.GetOrderByIdAsync(id.Value);
            if (orderDto == null)
            {
                return NotFound();
            }

            UpdateOrderDto updateOrderDto = new UpdateOrderDto
            {
                Id = orderDto.Id,
                Status = orderDto.Status,
            };

            return Ok(updateOrderDto);
        }

        [HttpGet("UploadView")]
        public IActionResult UploadView()
        {
            return Ok();
        }

        [HttpPost("Upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files["file"];
            var ssss = _localizer["Shared.Home"];

            try
            {
                if (file == null || file.Length <= 0)
                {
                    return Ok(false);
                }

                if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    return Ok(false);
                }

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string CustomerName = worksheet.Cells[row, 1].Text;
                            string CustomerPhone = worksheet.Cells[row, 2].Text;
                            string City = worksheet.Cells[row, 3].Text;
                            string CustomerAddress = worksheet.Cells[row, 4].Text;
                            string Quantity = worksheet.Cells[row, 5].Text;
                            string ProductTotalCost = worksheet.Cells[row, 6].Text;
                            string TotalCost = worksheet.Cells[row, 7].Text;
                            string Shipping_Cost = worksheet.Cells[row, 8].Text;
                            string ProductName = worksheet.Cells[row, 9].Text;
                            string ProductVariant = worksheet.Cells[row, 10].Text;
                            string OrderId = worksheet.Cells[row, 11].Text;
                            string Taager_ID = worksheet.Cells[row, 12].Text;

                            string OrderDate = worksheet.Cells[row, 13].Text;
                            string SKU = worksheet.Cells[row, 14].Text;
                            string ExtraData = worksheet.Cells[row, 15].Text;
                            string ExtraData2 = worksheet.Cells[row, 16].Text;
                            string UTMSource = worksheet.Cells[row, 17].Text;

                            string UTMCampaign = worksheet.Cells[row, 18].Text;
                            string PaymentMethod = worksheet.Cells[row, 19].Text;
                            string Coupon = worksheet.Cells[row, 20].Text;
                            string Discount = worksheet.Cells[row, 21].Text;
                            string Altphone = worksheet.Cells[row, 22].Text;
                            string CustomerNote = worksheet.Cells[row, 23].Text;

                            #region insert Products
                            Product product = new Product();

                            var _Product = DbContext.Products.Where(a => ProductName.Contains(a.Name)).FirstOrDefault();
                            if (_Product == null)
                            {
                                product.Name = ProductName.Trim();
                                product.NameAr = ProductName.Trim();
                                product.TotalCost = string.IsNullOrEmpty(ProductTotalCost) ? 0 : decimal.Parse(ProductTotalCost);
                                product.ShippingCost = string.IsNullOrEmpty(Shipping_Cost) ? 0 : decimal.Parse(Shipping_Cost);

                                DbContext.Products.Add(product);
                                await DbContext.SaveChangesAsync();
                            }
                            else
                            {
                                product = _Product;
                            }

                            #endregion

                            #region insert customer details

                            var _customer = DbContext.Customers.Where(a => CustomerName.Contains(a.Name) || CustomerPhone == a.Phone || a.Phone.StartsWith(CustomerPhone) || a.Phone.EndsWith(CustomerPhone))
                                .FirstOrDefault();
                            Customer customer = new();

                            if (_customer == null)
                            {
                                customer.Name = CustomerName.Trim();
                                customer.NameAr = CustomerName.Trim();
                                customer.Address = CustomerAddress.Trim();
                                customer.Phone = CustomerPhone.Trim();
                                DbContext.Customers.Add(customer);
                                await DbContext.SaveChangesAsync();
                            }
                            else
                            {
                                customer = _customer;
                            }

                            #endregion

                            #region insert order details

                            Order order = new();
                            order.CustomerId = customer.Id;
                            order.Status = InvoiceStatus.Pending;
                            DbContext.Orders.Add(order);
                            await DbContext.SaveChangesAsync();

                            OrderDetails orderDetails = new();
                            orderDetails.OrderId = order.Id;
                            orderDetails.ProductId = product.Id;
                            if (!string.IsNullOrEmpty(Quantity))
                            {
                                var Quan = Quantity.RemoveSpecialCharacters();
                                orderDetails.Quantity = int.Parse(Quan);
                            }
                            long _UTMCampaign = 0;
                            long.TryParse(UTMCampaign.Trim(), out _UTMCampaign);

                            orderDetails.UTMCampaign = string.IsNullOrEmpty(UTMCampaign) ? 0 : _UTMCampaign;
                            orderDetails.UTMSource = UTMSource;

                            DbContext.OrderDetails.Add(orderDetails);

                            #endregion

                            // Save to DB or process
                            await DbContext.SaveChangesAsync();

                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Ok(false);
            }

            return Ok(true);

            //return RedirectToAction(nameof(Index));
        }

        [HttpGet("ExportPdf/{id:int}")]
        public async Task<IActionResult> ExportPdf(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var orderDto = await _OrderService.GetOrderByIdAsync(id.Value);
            if (orderDto == null)
            {
                return NotFound();
            }

            var document = new InvoiceDocument
            {
                CustomerName = orderDto.Customer.Name,
                InvoiceNumber = orderDto.Id.ToString(),
                Items = orderDto.OrderDetails.ToList()
            };

            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", "Invoice.pdf");
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromQuery] int id, [FromQuery] InvoiceStatus status)
        {
            var ok = await _OrderService.ChangeStatusAsync(id, status);
            if (!ok)
                return NotFound();

            return Ok();
        }

        [HttpPost("UpdateArea")]
        public async Task<IActionResult> UpdateArea([FromBody] UpdateAreaDto model)
        {
            if (model == null || model.OrderId <= 0)
            {
                return Ok(new { success = false, message = "Invalid data" });
            }

            var result = await _OrderService.UpdateAreaAsync(model.OrderId, model.CityId);

            if (!result)
            {
                return Ok(new { success = false, message = "Order not found" });
            }

            return Ok(new { success = true, message = "Area updated successfully" });
        }

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto model)
        {
            if (model == null || model.OrderId <= 0)
            {
                return Ok(new { success = false, message = "Invalid data" });
            }

            if (!Enum.IsDefined(typeof(InvoiceStatus), model.Status))
            {
                return Ok(new { success = false, message = "Invalid status" });
            }

            bool result = await _OrderService.ChangeStatusAsync(model.OrderId, InvoiceStatus.Confirmed);

            if (!result)
            {
                return Ok(new { success = false, message = "Order not found" });
            }

            return Ok(new { success = true, message = "Status updated successfully" });
        }

        [HttpGet("AssignArea")]
        public async Task<IActionResult> AssignArea([FromQuery] int orderId = 0)
        {
            var order = orderId > 0
                ? await DbContext.Orders.FindAsync(orderId)
                : null;

            if (orderId > 0 && order == null)
            {
                return NotFound();
            }

            var selectedCity = order?.StatesId > 0
                ? await DbContext.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == order.StatesId)
                : null;
            var selectedStateId = selectedCity?.StateId ?? 0;

            var states = await StateService.GetAllAsync();
            var cities = selectedStateId > 0
                ? await StateService.GetCitiesByStateIdAsync(selectedStateId)
                : new List<CRM.WebApp.DTOs.HumanResources.CityDto>();
            var employees = await EmployeeService.GetAllEmployeesAsync();

            var statesList = states.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.NameAr,
                Selected = selectedStateId == s.Id
            }).ToList();

            var citiesList = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
                Selected = order?.StatesId == c.Id
            }).ToList();

            var employeesList = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name ?? $"{e.FirstName} {e.LastName}".Trim()
            }).ToList();

            var model = new UpdateAreaDto
            {
                OrderId = orderId,
                StateId = selectedStateId,
                AreaId = order?.StatesId ?? 0,
                EmployeeId = null
            };

            return Ok(new { model, statesList, citiesList, employeesList });
        }

        [HttpPost("AssignArea")]
        public async Task<IActionResult> AssignArea([FromBody] UpdateAreaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!model.EmployeeId.HasValue || model.EmployeeId.Value <= 0)
            {
                return BadRequest(new { message = "Employee is required" });
            }

            var assigned = await _OrderService.SaveAssignedLocationAsync(model);
            if (!assigned)
            {
                return BadRequest(new { message = "Unable to assign area" });
            }

            return Ok(model);
        }

        [HttpGet("GetCitiesByStateId")]
        public async Task<IActionResult> GetCitiesByStateId([FromQuery] int stateId)
        {
            if (stateId <= 0)
            {
                return Ok(Array.Empty<object>());
            }

            var cities = await StateService.GetCitiesByStateIdAsync(stateId);
            return Ok(cities.Select(c => new
            {
                id = c.Id,
                name = c.Name,
                nameAr = c.NameAr
            }));
        }

        [HttpGet("UsedAssignedLocations")]
        public async Task<IActionResult> UsedAssignedLocations()
        {
            var locations = await _OrderService.GetAllUsedAssignedLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("UsedAssignedLocationDetails/{id:int}")]
        public async Task<IActionResult> UsedAssignedLocationDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _OrderService.GetUsedAssignedLocationByIdAsync(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost("CreateUsedAssignedLocation")]
        public async Task<IActionResult> CreateUsedAssignedLocation([FromBody] UsedAssignedLocationViewModel model)
        {
            if (model.EmployeeId <= 0 || model.CountryId <= 0 || model.StatesId <= 0 || model.CityId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Employee, country, state and city are required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _OrderService.CreateUsedAssignedLocationAsync(model);

            return Ok(model);
        }

        [HttpPut("UsedAssignedLocation/{id:int}")]
        public async Task<IActionResult> EditUsedAssignedLocation(int id, [FromBody] UsedAssignedLocationViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (model.EmployeeId <= 0 || model.CountryId <= 0 || model.StatesId <= 0 || model.CityId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Employee, country, state and city are required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _OrderService.UpdateUsedAssignedLocationAsync(model);

            if (!result)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpDelete("UsedAssignedLocation/{id:int}")]
        public async Task<IActionResult> DeleteUsedAssignedLocationConfirmed(int id)
        {
            var result = await _OrderService.DeleteUsedAssignedLocationAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("Statistics")]
        public async Task<IActionResult> Statistics()
        {
            var employees = (await EmployeeService.GetAllEmployeesAsync()).Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name ?? $"{e.FirstName} {e.LastName}".Trim()
            }).ToList();

            return Ok(new { model = new StatisticsViewModel(), employees });
        }

        [HttpPost("Statistics")]
        public async Task<IActionResult> Statistics([FromBody] StatisticsViewModel model)
        {
            model.Statistics = await _OrderService.CalculateStatisticsAsync(model.EmployeeId, model.DateFrom, model.DateTo);

            var employees = (await EmployeeService.GetAllEmployeesAsync()).Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name ?? $"{e.FirstName} {e.LastName}".Trim(),
                Selected = model.EmployeeId.HasValue && e.Id == model.EmployeeId.Value
            }).ToList();

            return Ok(new { model, employees });
        }
    }
}