
using AutoMapper;
using CRM.WebApi.DbContext;
using CRM.WebApi.DbContext.EasyOrderModels;
using CRM.WebApi.EasyOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace CRM.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ApplicationContext DbContext;
        private readonly IMapper Mapper;

        public OrderController(
            ILogger<OrderController> logger,
            IMapper _Mapper,
            ApplicationContext _DbContext
        )
        {
            _logger = logger;
            DbContext = _DbContext;
            Mapper = _Mapper;
        }


        [HttpPost]
        [Route("receive")]
        public async Task<IActionResult> receive(object easyOrderDto)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("receive");
            string json = JsonSerializer.Serialize(easyOrderDto);

            _logger.LogInformation("Received Order JSON: {Json}", json);
            stopwatch.Stop();

            #region log request

            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Level = LogLevel.Information.ToString(),
                Message = $"HTTP {Request.Method} {Request.Path} responded {Response.StatusCode}",
                Url = $"{Request.Path}{Request.QueryString}",
                HttpMethod = Request.Method,
                UserName = "",
                ClientIP = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                StatusCode = Response.StatusCode,
                RequestBody = json,
                ResponseBody = json,
                Duration = stopwatch.ElapsedMilliseconds,
                Logger = "RequestLoggingMiddleware"
            };

            DbContext.Logs.Add(logEntry);
            await DbContext.SaveChangesAsync();

            #endregion

            JsonSerializerOptions? options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            EasyOrderDto? dto = JsonSerializer.Deserialize<EasyOrderDto>(json, options);

            bool IfExist = DbContext.EasyOrderRequests.Any(a => a.id == dto.id);
            if (IfExist)
            {
                return Ok("already exist");
            }
            EasyOrderRequest _EasyOrderRequest = Mapper.Map<EasyOrderRequest>(dto);

            foreach (var kvp in _EasyOrderRequest.cart_items)
            {
                var product = DbContext.EasyOrderProducts.FirstOrDefault(a => a.id == kvp.product_id);
                if (product != null)
                {
                    kvp.product = null;
                }
            }


            DbContext.EasyOrderRequests.Add(_EasyOrderRequest);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var ExceptionlogEntry = new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Level = LogLevel.Error.ToString(),
                    Message = $"HTTP {Request.HttpContext.Request.Method} {Request.HttpContext.Request.Path} responded {Request.HttpContext.Response.StatusCode}",
                    Url = $"{Request.HttpContext.Request.Path}{Request.HttpContext.Request.QueryString}",
                    HttpMethod = Request.HttpContext.Request.Method,
                    UserName = Request.HttpContext.User?.Identity?.Name,
                    ClientIP = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                    StatusCode = Request.HttpContext.Response.StatusCode,
                    RequestBody = JsonSerializer.Serialize(ex),
                    ResponseBody = JsonSerializer.Serialize(ex),
                    Duration = stopwatch.ElapsedMilliseconds,
                    Logger = "RequestLoggingMiddleware"
                };


                DbContext.Logs.Add(logEntry);
                await DbContext.SaveChangesAsync();
            }



            #region insert Campaign details

            string utm_campaign = dto?.utm_campaign?.ToString() ?? string.Empty;

            #endregion

            foreach (var item in dto.cart_items)
            {
                #region insert Products
                Product product = new Product();

                Product? _Product = DbContext.Products.Where(a => a.EasyOrderProductId.HasValue && a.EasyOrderProductId.Value == item.product_id).FirstOrDefault();
                if (_Product == null)
                {
                    product.Name = item?.product?.name.Trim() ?? string.Empty;
                    product.NameAr = item?.product?.name?.Trim() ?? string.Empty;

                    product.TotalCost = item.product.price.HasValue && item.product.price > 0 ? item.product.price.Value : 0;
                    product.ShippingCost = item.product.sale_price.HasValue && item.product.sale_price > 0 ? item.product.sale_price.Value : 0;
                    product.DateCreated = DateTime.Now;
                    product.DateModified = DateTime.Now;
                    product.Description = item.product?.description;
                    product.QuantityInStock = item.product.quantity.HasValue ? item.product.quantity.Value : 0;
                    product.EasyOrderProductId = item.product_id;

                    DbContext.Products.Add(product);
                    await DbContext.SaveChangesAsync();
                }
                else
                {
                    product = _Product;
                }


                #endregion

                #region insert customer details


                Customer? _customer = DbContext.Customers.Where(a => dto.full_name.Contains(a.Name)
                || dto.phone == a.Phone
                || a.Phone.StartsWith(dto.phone)
                || a.Phone.EndsWith(dto.phone))
                    .FirstOrDefault();
                Customer customer = new();

                if (_customer == null)
                {
                    customer.Name = dto?.full_name?.Trim() ?? string.Empty;
                    customer.NameAr = dto?.full_name?.Trim() ?? string.Empty;
                    customer.Address = dto?.address?.Trim() ?? string.Empty;
                    customer.Phone = dto?.phone?.Trim() ?? string.Empty;
                    customer.DateCreated = DateTime.Now;
                    customer.DateModified = DateTime.Now;
                    customer.CreditTerm = CreditTerm.Prepaid;
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
                order.DateCreated = DateTime.Now;
                order.DateModified = DateTime.Now;
                order.Status = InvoiceStatus.Open;
                order.EasyOrderRequestId = _EasyOrderRequest.id;

                DbContext.Orders.Add(order);
                await DbContext.SaveChangesAsync();


                OrderDetails orderDetails = new();
                orderDetails.OrderId = order.Id;
                orderDetails.ProductId = product.Id;
                orderDetails.Quantity = item.quantity.GetValueOrDefault();
                orderDetails.DateCreated = DateTime.Now;
                orderDetails.DateModified = DateTime.Now;
                long _UTMCampaign = 0;
                long.TryParse(dto?.utm_campaign, out _UTMCampaign);
                orderDetails.UTMCampaign = string.IsNullOrEmpty(dto?.utm_campaign) ? 0 : _UTMCampaign;
                orderDetails.UTMSource = dto?.utm_source;
                DbContext.OrderDetails.Add(orderDetails);
                await DbContext.SaveChangesAsync();

                #endregion
            }
            return Ok("Added");
        }



        [HttpPost]
        [Route("XX_receive")]
        public async Task<IActionResult> XXX_receive(EasyOrderDto easyOrderDto)
        {
            _logger.LogInformation("receive");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.Select(a => a.Errors);
                return BadRequest();
            }

            _logger.LogInformation("Received Order JSON: {Json}", JsonSerializer.Serialize(easyOrderDto));
            //return Ok();


            #region insert Campaign details

            CampaignTypes campaignTypes = new();
            var _CampaignTypes = DbContext.CampaignTypes.Where(a => a.Name == easyOrderDto.utm_source).FirstOrDefault();
            if (_CampaignTypes == null)
            {
                campaignTypes.DateCreated = DateTime.Now;
                campaignTypes.DateModified = DateTime.Now;
                campaignTypes.Name = easyOrderDto?.utm_source;
                campaignTypes.NameAr = easyOrderDto?.utm_source;

                DbContext.CampaignTypes.Add(campaignTypes);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                campaignTypes = _CampaignTypes;
            }


            string utm_campaign = easyOrderDto.utm_campaign.ToString();
            //Campaign campaign = new();
            ////var _Campaign = DbContext.Campaigns.Where(a => a.ProviderSerial.Length > utm_campaign.Length || (a.ProviderSerial.Length == utm_campaign.Length &&
            ////                                    string.CompareOrdinal(a.ProviderSerial, utm_campaign) > 0)).ToList();


            //var SqlQuery = $@"
            //                                        SELECT *
            //                                        FROM Campaigns
            //                                        WHERE  ProviderSerial = '{utm_campaign}'
            //                                    ";
            //var _Campaign = DbContext.Campaigns
            //                                    .FromSqlRaw(SqlQuery)
            //                                    .ToList();


            //if (!_Campaign.Any())
            //{
            //    campaign.ProviderSerial = easyOrderDto.utm_campaign;

            //    campaign.Name = utm_campaign;
            //    campaign.Description = "test";
            //    campaign.ProviderSerial = easyOrderDto.utm_source;
            //    campaign.StartDate = easyOrderDto.created_at;
            //    campaign.Status = CampaignStatus.Active;
            //    campaign.DateCreated = DateTime.Now;
            //    campaign.DateModified = DateTime.Now;
            //    campaign.CampaignTypesId = campaignTypes.Id;

            //    DbContext.Campaigns.Add(campaign);
            //    try
            //    {
            //        string value = "120229047449680659";
            //        long result = Convert.ToInt64(value);

            //        await DbContext.SaveChangesAsync();

            //    }
            //    catch (OverflowException)
            //    {
            //        // Handle overflow - number too large even for long
            //    }
            //    catch (FormatException)
            //    {
            //        // Handle invalid format
            //    }

            //}
            //else
            //{
            //    campaign = _Campaign.FirstOrDefault();
            //}

            #endregion



            foreach (var item in easyOrderDto.cart_items)
            {
                #region insert Products
                Product product = new Product();

                var _Product = DbContext.Products.Where(a => item.product.name.Contains(a.Name)).FirstOrDefault();
                if (_Product == null)
                {
                    product.Name = item.product.name.Trim();
                    product.NameAr = item.product.name.Trim();

                    product.TotalCost = item.product.price > 0 ? 0 : item.product.price.Value;
                    product.ShippingCost = item.product.sale_price > 0 ? 0 : item.product.sale_price.Value;
                    product.DateCreated = DateTime.Now;
                    product.DateModified = DateTime.Now;
                    DbContext.Products.Add(product);
                    await DbContext.SaveChangesAsync();
                }
                else
                {
                    product = _Product;
                }



                #endregion


                #region insert customer details


                var _customer = DbContext.Customers.Where(a => easyOrderDto.full_name.Contains(a.Name) || easyOrderDto.phone == a.Phone || a.Phone.StartsWith(easyOrderDto.phone) || a.Phone.EndsWith(easyOrderDto.phone))
                    .FirstOrDefault();
                Customer customer = new();

                if (_customer == null)
                {
                    customer.Name = easyOrderDto.full_name.Trim();
                    customer.NameAr = easyOrderDto.full_name.Trim();
                    customer.Address = easyOrderDto.address.Trim();
                    customer.Phone = easyOrderDto.phone.Trim();
                    customer.DateCreated = DateTime.Now;
                    customer.DateModified = DateTime.Now;
                    customer.CreditTerm = CreditTerm.Prepaid;
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
                order.DateCreated = DateTime.Now;
                order.DateModified = DateTime.Now;
                order.Status = InvoiceStatus.Open;
                DbContext.Orders.Add(order);
                await DbContext.SaveChangesAsync();


                OrderDetails orderDetails = new();
                orderDetails.OrderId = order.Id;
                orderDetails.ProductId = product.Id;
                orderDetails.Quantity = item.quantity.GetValueOrDefault();
                orderDetails.DateCreated = DateTime.Now;
                orderDetails.DateModified = DateTime.Now;


                long _UTMCampaign = 0;
                long.TryParse(easyOrderDto?.utm_campaign, out _UTMCampaign);



                orderDetails.UTMCampaign = string.IsNullOrEmpty(easyOrderDto?.utm_campaign) ? 0 : _UTMCampaign;
                orderDetails.UTMSource = easyOrderDto?.utm_source;
                DbContext.OrderDetails.Add(orderDetails);
                await DbContext.SaveChangesAsync();

                #endregion

            }




            // inned to serialize the EasyOrderDto to a message queue or process it as needed
            // For example, you can use a message queue service like RabbitMQ, Azure Service Bus, etc.
            // Here, we will just log the received order for demonstration purposes

            _logger.LogInformation("Received Order JSON: {Json}", JsonSerializer.Serialize(easyOrderDto));
            return Ok();
        }



        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var url = "https://api.easy-orders.net/api/v1/external-apps/products";

            // Your API key
            var apiKey = "a49dae64-150d-4b34-b6e7-a3112b28fa78";

            var httpClient = new HttpClient();

            // Add Header
            httpClient.DefaultRequestHeaders.Add("Api-Key", apiKey);

            // Call API
            var response = await httpClient.GetAsync(url);

            // Ensure success
            response.EnsureSuccessStatusCode();

            // Read body
            var json = await response.Content.ReadAsStringAsync();

            // Deserialize
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            //var products = JsonSerializer.Deserialize<List<ProductDto>>(json, options);


            return Ok(json);

        }



        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var url = "https://api.easy-orders.net/api/v1/external-apps/categories";

            // Your API key
            var apiKey = "a49dae64-150d-4b34-b6e7-a3112b28fa78";

            var httpClient = new HttpClient();

            // Add Header
            httpClient.DefaultRequestHeaders.Add("Api-Key", apiKey);

            // Call API
            var response = await httpClient.GetAsync(url);

            // Ensure success
            response.EnsureSuccessStatusCode();

            // Read body
            var json = await response.Content.ReadAsStringAsync();

            // Deserialize
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            //var products = JsonSerializer.Deserialize<List<ProductDto>>(json, options);


            return Ok(json);

        }


        [HttpGet]
        [Route("TestConnection")]
        public async Task<IActionResult> TestConnection()
        {
            var result = await DbContext.Logs.FirstOrDefaultAsync();
            return Ok(result);
        }

    }
}
