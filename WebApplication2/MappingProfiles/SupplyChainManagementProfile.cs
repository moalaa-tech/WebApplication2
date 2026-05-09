using AutoMapper;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;
using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.ViewModels.SupplyChainManagement.ForecastData;
using CRM.WebApp.ViewModels.SupplyChainManagement.Purchase;
using CRM.WebApp.ViewModels.SupplyChainManagement.Supplier;

namespace CRM.WebApp.MappingProfiles
{
    public class SupplyChainManagementProfile : Profile
    {
        public SupplyChainManagementProfile()
        {
            CreateMap<DemandPlan, DemandPlanDto>().ReverseMap();
            CreateMap<DemandPlanCreateDto, DemandPlan>();
            CreateMap<DemandPlanUpdateDto, DemandPlan>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Shipping, ShippingDto>().ReverseMap();
            CreateMap<ShippingCreateDto, Shipping>();
            CreateMap<ShippingUpdateDto, Shipping>();

            // Other mappings...

            // Supplier mappings
            CreateMap<Supplier, SupplierDto>();
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();
            CreateMap<SupplierDto, SupplierViewModel>();
            CreateMap<Supplier, SupplierViewModel>();

            // Purchase Order mappings
            CreateMap<PurchaseOrder, PurchaseOrderDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));

            CreateMap<CreatePurchaseOrderDto, PurchaseOrder>();
            CreateMap<PurchaseOrderDto, PurchaseOrderViewModel>();

            // Purchase Order Item mappings
            CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>();
            CreateMap<CreatePurchaseOrderItemDto, PurchaseOrderItem>();
            CreateMap<PurchaseOrderItemDto, PurchaseOrderItemViewModel>();


            CreateMap<ForecastData, ForecastDataDto>()
               .ForMember(dest => dest.DemandPlanName, opt => opt.MapFrom(src => src.DemandPlan.PlanName))
               .ForMember(dest => dest.SupplyChainEventName, opt => opt.MapFrom(src => src.SupplyChainEvent.EventName))
               .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.ProductName))
               .ForMember(dest => dest.ShippingReference, opt => opt.MapFrom(src => src.Shipping.TrackingNumber))
               .ForMember(dest => dest.FreightReference, opt => opt.MapFrom(src => src.Freight.FreightNumber));
               //.ForMember(dest => dest.HistoricalDataPeriod, opt => opt.MapFrom(src => src.HistoricalData.Period)
               

            CreateMap<CreateForecastDataDto, ForecastData>();
            CreateMap<UpdateForecastDataDto, ForecastData>();

            CreateMap<ForecastDataDto, ForecastDataViewModel>();
            CreateMap<ForecastData, ForecastDataViewModel>();

            CreateMap<CreateForecastDataViewModel, CreateForecastDataDto>();
            CreateMap<EditForecastDataViewModel, UpdateForecastDataDto>();

        }
    }
}
