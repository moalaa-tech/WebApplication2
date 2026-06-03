using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.WebApp.Controllers.InventoryManagment;
using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.DTOs.OrderDetails;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.InventoryManagement.Order;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CRM.WebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDetails> OrderDetailsRepo;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<UsedAssignedLocation> _usedAssignedLocationRepository;

        public OrderService(
            IMapper mapper,
            IRepository<Order> orderRepository,
            IRepository<OrderDetails> orderDetailsRepo,
            IRepository<City> cityRepository,
            IRepository<UsedAssignedLocation> usedAssignedLocationRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            OrderDetailsRepo = orderDetailsRepo;
            _cityRepository = cityRepository;
            _usedAssignedLocationRepository = usedAssignedLocationRepository;
        }



        public async Task<PaginatedList<OrderDto>> GetAllOrdersAsync(OrderViewModel model)
        {
            var orders = _orderRepository.GetAllAsync(
                include: q => q
                    .Include(o => o.Customer)
                    .Include(o => o.OrderDetails).ThenInclude(i => i.Product)
            ).OrderByDescending(a => a.Id).AsQueryable();

            if (model.DateFrom.HasValue)
            {
                orders = orders.Where(a => a.DateCreated.Value.Date == DateTime.Now.Date);
            }

            if (model.InvoiceNumber.HasValue)
                orders = orders.Where(x => x.Id == model.InvoiceNumber.Value);

            if (model.Status.HasValue)
                orders = orders.Where(x => x.Status == model.Status);

            if (model.ProductId.HasValue)
                orders = orders.Where(x => x.OrderDetails.Any(a => a.ProductId == model.ProductId));

            if (model.EmployeeId.HasValue)
                orders = orders.Where(x => x.AssignedToId.HasValue && x.AssignedToId.Value == model.EmployeeId);

            if (model.DateFrom.HasValue)
                orders = orders.Where(x => x.DateCreated >= model.DateFrom.Value);

            if (model.DateTo.HasValue)
            {
                var to = model.DateTo.Value.Date.AddDays(1).AddSeconds(-1);
                orders = orders.Where(x => x.DateCreated <= to);
            }


            if (model.StateId.HasValue)
            {
                orders = orders.Where(x => x.StatesId == model.StateId);
            }

            if (model.CustomerId.HasValue)
            {
                orders = orders.Where(x => x.CustomerId == model.CustomerId);
            }



            var data = orders.Select(m => new OrderDto
            {
                Id = m.Id,
                Status = m.Status,
                Customer = new CustomerDto
                {
                    //Id = m.Customer.Id,
                    Name = m.Customer.Name,
                    NameAr = m.Customer.NameAr,
                    //Email = m.Customer.Email,
                    //Phone = m.Customer.Phone,
                    Address = m.Customer.Address,
                },
                DateCreated = m.DateCreated.Value,
                TotalAmount = m.OrderDetails.Sum(od => od.Product.TotalCost),
                StatesId = m.StatesId,
                CityStateId = _cityRepository.GetAll()
                    .Where(c => c.Id == m.StatesId)
                    .Select(c => (int?)c.StateId)
                    .FirstOrDefault()
            });

            int TotalCount = await data.CountAsync();

            //data = data.Skip((model.PageIndex - 1) * model.pageSize).Take(model.pageSize);

            var dtoR = _mapper.Map<IEnumerable<OrderDto>>(data);
            var result = PaginatedList<OrderDto>.Create(data, model.PageIndex, model.pageSize);
            result.TotalCount = TotalCount;
            return result;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            IQueryable<Order> orders = _orderRepository.GetAllAsync(a => a.Id == id,
                include: q => q
                .Include(a => a.EasyOrderRequest).ThenInclude(a => a.cart_items)
                    .Include(o => o.Customer)
                    .Include(o => o.OrderDetails).ThenInclude(i => i.Product)
            );
            var mm = orders.ToList();

            IQueryable<OrderDto> data = orders.Select(m => new OrderDto
            {
                Id = m.Id,
                Status = m.Status,
                Description = m.Description,
                Customer = new CustomerDto
                {
                    Id = m.Customer.Id,
                    Name = m.Customer.Name,
                    NameAr = m.Customer.NameAr,
                    Email = m.Customer.Email,
                    Phone = m.Customer.Phone,
                    Address = m.Customer.Address,
                },
                DateCreated = m.DateCreated.Value,
                TotalAmount = m.EasyOrderRequest.total_cost.GetValueOrDefault(0),
                Note = m.EasyOrderRequest.note,
                OrderDetails = m.OrderDetails.Select(s => new OrderDetailsDto
                {
                    ItemsCount = s.Quantity,
                    Note = s.Note,
                    UTMCampaign = s.UTMCampaign,
                    UTMSource = s.UTMSource,
                    Product = new ProductDto
                    {
                        Id = s.Id,
                        Name = s.Product.Name ?? string.Empty,
                        NameAr = s.Product.NameAr,
                        ProductTypeName = s.Product.ProductType.NameAr,
                        Description = s.Product.Description,
                        TotalCost = s.Product.TotalCost,
                        SalesPrice = s.Product.TotalCost
                    },
                }),
                StatesId = m.StatesId,
                //OrderDetails = m.EasyOrderRequest.cart_items.Select(s => new OrderDetailsDto
                //{
                //    ItemsCount = s.quantity.GetValueOrDefault(0),
                //    Note = m.EasyOrderRequest.note,
                //    UTMCampaign = !string.IsNullOrEmpty(m.EasyOrderRequest.utm_campaign) ? Convert.ToInt64(m.EasyOrderRequest.utm_campaign) : 0,
                //    UTMSource = m.EasyOrderRequest.utm_source,
                //    ProductId = s.product_id.Value,
                //    Product = new ProductDto
                //    {
                //        // Id = s.Id,
                //        Name = s.product.name ?? string.Empty,
                //        NameAr = s.product.name,
                //        ProductTypeName = s.product.slug,
                //        Description = s.product.description,
                //        TotalCost = s.product.Price.GetValueOrDefault(0),
                //        SalesPrice = s.product.sale_price.GetValueOrDefault(0),
                //    },
                //})
            });

            OrderDto? _date = data.FirstOrDefault();
            return _date;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<UpdateOrderDto> UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await _orderRepository.GetByIdAsync(updateOrderDto.Id);

            // var order = _mapper.Map<Order>(updateOrderDto);
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();

            return updateOrderDto;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync();

        }

        public async Task<bool> OrderExistsAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return false;

            return true;
        }

        public async Task<IEnumerable<OrderDto>> Search(string search = null)
        {
            var items = _orderRepository.GetAsync(a => a.Customer.Name.Contains(search) || a.Id.ToString().Contains(search));
            return _mapper.Map<IEnumerable<OrderDto>>(items);
        }

        public async Task<bool> ChangeStatusAsync(int id, InvoiceStatus newStatus)
        {
            var invoice = await _orderRepository.GetByIdAsync(id);
            if (invoice == null) return false;

            invoice.Status = newStatus;

            _orderRepository.Update(invoice);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveAssignedLocationAsync(UpdateAreaDto model)
        {
            var city = await _cityRepository.GetAllAsync(
                c => c.Id == model.AreaId,
                include: q => q.Include(c => c.State))
                .FirstOrDefaultAsync();

            if (city?.State == null) return false;

            var assignedLocation = new UsedAssignedLocation
            {
                EmployeeId = model.EmployeeId.Value,
                CountryId = city.State.CountryId,
                StatesId = city.StateId,
                CityId = city.Id
            };


            await _usedAssignedLocationRepository.AddAsync(assignedLocation);
            await _usedAssignedLocationRepository.SaveChangesAsync();

            return true;
        }


        public async Task<bool> AddProductToOrderAsync(UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(dto.Id);
            if (order == null) return false;

            foreach (var item in dto.OrderDetails)
            {
                OrderDetails _OrderDetails = new OrderDetails
                {
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.ItemsCount,
                    UTMCampaign = item.UTMCampaign,
                    UTMSource = item.UTMSource
                };


                await OrderDetailsRepo.AddAsync(_OrderDetails);
                await OrderDetailsRepo.SaveChangesAsync();
            }
            return true;
        }

        public async Task<StatisticsData> CalculateStatisticsAsync(int? employeeId, DateTime? dateFrom, DateTime? dateTo)
        {
            var orders = _orderRepository.GetAllAsync(
                include: q => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
            ).AsQueryable();

            // Filter by employee (AssignedToId)
            if (employeeId.HasValue)
            {
                orders = orders.Where(o => o.AssignedToId == employeeId.Value);
            }

            // Filter by date range
            if (dateFrom.HasValue)
            {
                orders = orders.Where(o => o.DateCreated >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                var toDate = dateTo.Value.Date.AddDays(1).AddSeconds(-1);
                orders = orders.Where(o => o.DateCreated <= toDate);
            }

            // Calculate orders per month
            var ordersPerMonth = await orders
                .Where(o => o.DateCreated.HasValue)
                .GroupBy(o => new { o.DateCreated.Value.Year, o.DateCreated.Value.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .OrderByDescending(g => g.Year)
                .ThenByDescending(g => g.Month)
                .ToListAsync();

            var monthNames = new[] { "January", "February", "March", "April", "May", "June",
                                     "July", "August", "September", "October", "November", "December" };

            return new StatisticsData
            {
                TotalOrders = await orders.CountAsync(),
                PendingOrders = await orders.Where(o => o.Status == InvoiceStatus.Pending).CountAsync(),
                ConfirmedOrders = await orders.Where(o => o.Status == InvoiceStatus.Confirmed).CountAsync(),
                PaidOrders = await orders.Where(o => o.Status == InvoiceStatus.Paid).CountAsync(),
                DeliveredOrders = await orders.Where(o => o.Status == InvoiceStatus.Delivered).CountAsync(),
                CanceledOrders = await orders.Where(o => o.Status == InvoiceStatus.Canceled).CountAsync(),
                TotalAmount = await orders.SumAsync(o => o.OrderDetails.Sum(od => od.Product.TotalCost)),
                OrdersPerMonth = ordersPerMonth.Select(x => new MonthlyOrders
                {
                    Year = x.Year,
                    Month = x.Month,
                    MonthName = monthNames[x.Month - 1],
                    OrderCount = x.Count
                }).ToList()
            };
        }


    }
}
