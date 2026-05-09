using AutoMapper;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.DTOs.OrderDetails;
using CRM.WebApp.Paging;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.InventoryManagement.Order;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CRM.WebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDetails> OrderDetailsRepo;
        public OrderService(IMapper mapper, IRepository<Order> orderRepository, IRepository<OrderDetails> orderDetailsRepo)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            OrderDetailsRepo = orderDetailsRepo;
        }



        public async Task<PaginatedList<OrderDto>> GetAllOrdersAsync(OrderViewModel model)
        {
            var orders = _orderRepository.GetAllAsync(
                include: q => q
                    .Include(o => o.Customer)
                    .Include(o => o.OrderDetails).ThenInclude(i => i.Product)
            ).OrderByDescending(a=>a.Id).AsQueryable();

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
                    //Address = m.Customer.Address,                                      
                },
                DateCreated = m.DateCreated.Value,
                TotalAmount = m.OrderDetails.Sum(od => od.Product.TotalCost),

            });

            var TotalCount = await data.CountAsync();
            var dtoR = _mapper.Map<IEnumerable<OrderDto>>(data);
            var result = PaginatedList<OrderDto>.Create(dtoR, model.PageIndex, model.pageSize);
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
                //OrderDetails = m.OrderDetails.Select(s => new OrderDetailsDto
                //{
                //    ItemsCount = s.Quantity,
                //    Note = s.Note,
                //    UTMCampaign = s.UTMCampaign,
                //    UTMSource = s.UTMSource,
                //    Product = new ProductDto
                //    {
                //        Name = s.Product.Name,
                //        NameAr = s.Product.NameAr,
                //        Id = s.Id,
                //        ProductTypeName = s.Product.ProductType.NameAr
                //    },
                //})

                OrderDetails = m.EasyOrderRequest.cart_items.Select(s => new OrderDetailsDto
                {
                    ItemsCount = s.quantity.GetValueOrDefault(0),
                    Note = m.EasyOrderRequest.note,
                    UTMCampaign = !string.IsNullOrEmpty(m.EasyOrderRequest.utm_campaign) ? Convert.ToInt64(m.EasyOrderRequest.utm_campaign) : 0,
                    UTMSource = m.EasyOrderRequest.utm_source,
                    Product = new ProductDto
                    {
                        // Id = s.Id,
                        Name = s.product.name ?? string.Empty,
                        NameAr = s.product.name,
                        ProductTypeName = s.product.slug,
                        Description = s.product.description,
                        TotalCost = s.product.Price.GetValueOrDefault(0),
                        SalesPrice = s.product.sale_price.GetValueOrDefault(0),
                    },
                })
            });

            OrderDto? _date = data.FirstOrDefault();

            //return _mapper.Map<OrderDto>();
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


    }
}
