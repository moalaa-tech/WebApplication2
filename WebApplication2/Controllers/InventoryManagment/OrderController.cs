using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Entities.HR;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Enums;
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
using CRM.WebApp.ViewModels.InventoryManagement.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using QuestPDF.Fluent;
using System.Threading.Tasks;

namespace CRM.WebApp.Controllers.InventoryManagment
{
    public class OrderController : Controller
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
            IStringLocalizer<HomeController> localizer
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
        }


        [HttpGet]
        public async Task<IActionResult> Index(OrderViewModel model)
        {
            var States = await StateService.GetAllAsync();
            var Contries = await CountryService.GetAllAsync();
            var Products = await ProductService.GetAllProductsAsync();
            var Users = await AuthenticationService.GetAllUserAsync();
            var Customers = await AuthenticationService.GetAllUserAsync();

            ViewBag.States = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // or s.Id if you want to filter by Id
                Text = s.NameAr,
                Selected = model.StateId == s.Id
            }).ToList();

            ViewBag.Countries = Contries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
                Selected = model.CountryId == c.Id
            }).ToList();

            ViewBag.Products = Products.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
                Selected = model.ProductId == c.Id
            }).ToList();


            ViewBag.Customer = Customers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FirstName,
                Selected = model.CustomerId == c.Id
            }).ToList();


            ViewBag.Users = Users.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FirstName ?? c.Email,
                Selected = model.EmployeeId == c.Id
            }).ToList();



            PaginatedList<OrderDto> orders = await _OrderService.GetAllOrdersAsync(model);
            model.Result = orders;
            return View(model);
        }

        [HttpGet]
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

            return View(order);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var States = await StateService.GetAllAsync();
            var Contries = await CountryService.GetAllAsync();

            ViewBag.States = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // or s.Id if you want to filter by Id
                Text = s.NameAr,
            }).ToList();

            ViewBag.Countries = Contries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
            }).ToList();


            ViewBag.customer = new SelectList(await CustomerService.GetAllCustomersAsync(), "Id", "Name");
            ViewBag.products = new SelectList(await ProductService.GetAllProductsAsync(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderDto createOrderDto)
        {
            if (ModelState.IsValid)
            {
                await _OrderService.CreateOrderAsync(createOrderDto);
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            var States = await StateService.GetAllAsync();
            var Contries = await CountryService.GetAllAsync();

            ViewBag.customer = new SelectList(await CustomerService.GetAllCustomersAsync(), "Id", "Name");
            ViewBag.products = new SelectList(await ProductService.GetAllProductsAsync(), "Id", "Name");
            ViewBag.States = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // or s.Id if you want to filter by Id
                Text = s.NameAr,
            }).ToList();

            ViewBag.Countries = Contries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
            }).ToList();
            return View(createOrderDto);
        }


        [HttpGet]
        public async Task<IActionResult> AddProduct(int id)
        {
            ViewBag.Products = new SelectList(await ProductService.GetAllProductsAsync(), "Id", "Name");

            //var dto = await _OrderService.GetAddProductDtoAsync(id);
            return PartialView("_AddProduct");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(UpdateOrderDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data" });

            var result = await _OrderService.UpdateOrderAsync(model);

            if (result == null)
                return Json(new { success = false, message = "Failed to add product" });

            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
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
                OrderNumber = orderDto.OrderNumber,
                OrderDate = orderDto.DateCreated,
                TotalAmount = orderDto.TotalAmount,
                OrderDetails = orderDto.OrderDetails
            };

            ViewBag.customer = new SelectList(await CustomerService.GetAllCustomersAsync(), "Id", "Name");
            ViewBag.products = new SelectList(await ProductService.GetAllProductsAsync(), "Id", "Name");
            var States = await StateService.GetAllAsync();
            var Contries = await CountryService.GetAllAsync();

            ViewBag.States = States.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // or s.Id if you want to filter by Id
                Text = s.NameAr,
            }).ToList();

            ViewBag.Countries = Contries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.NameAr,
            }).ToList();


            return View(updateOrderDto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateOrderDto updateOrderDto)
        {
            if (id != updateOrderDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag["ProductTypes"] = new SelectList(await ProductTypeService.GetAllAsync(), "Id", "Name");
                ViewBag.customer = new SelectList(await CustomerService.GetAllCustomersAsync(), "Id", "Name");
                ViewBag.products = new SelectList(await ProductService.GetAllProductsAsync(), "Id", "Name");

                var States = await StateService.GetAllAsync();
                var Contries = await CountryService.GetAllAsync();

                ViewBag.States = States.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(), // or s.Id if you want to filter by Id
                    Text = s.NameAr,
                }).ToList();

                ViewBag.Countries = Contries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NameAr,
                }).ToList();

                return View(updateOrderDto);
            }

            if (!await _OrderService.OrderExistsAsync(id))
            {
                return NotFound();
            }
           var mm = await _OrderService.UpdateOrderAsync(updateOrderDto);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            TempData["LoaderOn"] = "LoaderOn";
            var ssss = _localizer["Shared.Home"];

            try
            {
                if (file == null || file.Length <= 0)
                {
                    TempData["LoaderOff"] = "LoaderOff";
                    TempData["ToasterType"] = "info";
                    TempData["ToasterMessage"] = "File is empty.";
                    return View();
                }

                if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["LoaderOff"] = "LoaderOff";
                    TempData["ToasterType"] = "error";
                    TempData["ToasterMessage"] = "Invalid file type.";
                    return View();
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
                            order.Status = InvoiceStatus.Open;
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
                TempData["LoaderOff"] = "LoaderOff";

                TempData["ToasterType"] = "error";
                TempData["ToasterMessage"] = _localizer["Shared.Home"];
                return Ok(false);
            }

            TempData["LoaderOff"] = "LoaderOff";
            return Ok(true);

            //return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, InvoiceStatus status)
        {
            var ok = await _OrderService.ChangeStatusAsync(id, status);
            if (!ok)
                return NotFound();

            TempData["Success"] = "Status updated successfully";
            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
