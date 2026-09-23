import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface SelectListItem {
  value: string;
  text: string;
  selected?: boolean;
}

export interface ProductDto {
  id?: number | null;
  name?: string | null;
  nameAr?: string | null;
  totalCost?: number | null;
  shippingCost?: number | null;
  freightCost?: number | null;
  salesPrice?: number | null;
  productTypeId?: number | null;
  productTypeName?: string | null;
  imageFile?: string | null;
  description?: string | null;
  currentStock?: number | null;
  minimumStockLevel?: number | null;
  sku?: string | null;
}

export interface CustomerDto {
  id: number;
  name?: string | null;
  nameAr?: string | null;
  description?: string | null;
  email?: string | null;
  address?: string | null;
  phone?: string | null;
}

export interface OrderDetailsDto {
  orderId: number;
  productId: number;
  product: ProductDto;
  itemsCount: number;
  utmCampaign: number;
  note?: string | null;
  utmSource?: string | null;
}

export interface OrderDto {
  id: number;
  description?: string | null;
  customerId?: number | null;
  customer?: CustomerDto | null;
  orderNumber?: string | null;
  status: number;
  dateCreated: string;
  totalAmount: number;
  statesId?: number | null;
  countryId?: number | null;
  cityId?: number | null;
  note?: string | null;
  orderDetails?: OrderDetailsDto[] | null;
}

export interface OrderDetailCreateDto {
  productId: number;
  quantity: number;
}

export interface CreateOrderDto {
  description?: string | null;
  customerId: number;
  totalAmount?: number | null;
  items: OrderDetailCreateDto[];
}

export interface UpdateOrderDto {
  id: number;
  description?: string | null;
  note?: string | null;
  customerId?: number | null;
  productId?: number | null;
  status: number;
  orderNumber?: string | null;
  orderDate: string;
  totalAmount: number;
  orderDetails?: OrderDetailsDto[] | null;
  customer?: CustomerDto | null;
}

export interface UpdateAreaDto {
  orderId: number;
  cityId: number;
  stateId: number;
  areaId: number;
  employeeId?: number | null;
}

export interface UpdateStatusDto {
  orderId: number;
  status: number;
}

export interface UsedAssignedLocationViewModel {
  id: number;
  employeeId: number;
  employeeName?: string | null;
  countryId: number;
  countryName?: string | null;
  statesId: number;
  stateName?: string | null;
  cityId: number;
  cityName?: string | null;
}

export interface OrderViewModel {
  invoiceNumber?: number | null;
  search?: string | null;
  status?: number | null;
  employeeId?: number | null;
  customerId?: number | null;
  dateFrom?: string | null;
  dateTo?: string | null;
  productId?: number | null;
  states?: number | null;
  stateId?: number | null;
  countryId?: number | null;
  cityId?: number | null;
  pageIndex: number;
  pageSize: number;
  result?: OrderDto[] | null;
}

export interface TodayOrdersResult {
  model: OrderViewModel;
  allStates: SelectListItem[];
  states: SelectListItem[];
  cities: SelectListItem[];
}

export interface OrdersResult {
  model: OrderViewModel;
  states: SelectListItem[];
  countries: SelectListItem[];
  products: SelectListItem[];
  customer: SelectListItem[];
  users: SelectListItem[];
}

export interface ActionResponse {
  success: boolean;
  message?: string;
}

export interface AssignAreaResult {
  model: UpdateAreaDto;
  statesList: SelectListItem[];
  citiesList: SelectListItem[];
  employeesList: SelectListItem[];
}

export interface CityOption {
  id: number;
  name: string;
  nameAr: string;
}

export interface MonthlyOrders {
  year: number;
  month: number;
  monthName: string;
  orderCount: number;
}

export interface StatisticsData {
  totalOrders: number;
  pendingOrders: number;
  confirmedOrders: number;
  paidOrders: number;
  deliveredOrders: number;
  canceledOrders: number;
  totalAmount: number;
  ordersPerMonth: MonthlyOrders[];
}

export interface StatisticsViewModel {
  employeeId?: number | null;
  dateFrom?: string | null;
  dateTo?: string | null;
  statistics?: StatisticsData | null;
}

export interface StatisticsResult {
  model: StatisticsViewModel;
  employees: SelectListItem[];
}

@Injectable({ providedIn: 'root' })
export class OrderService extends ApiClientBase {
  protected readonly endpoint = 'Order';

  constructor(http: HttpClient) {
    super(http);
  }

  getTodayOrders(model: OrderViewModel): Observable<TodayOrdersResult> {
    return this.http.get<TodayOrdersResult>(this.url('/TodayOrders'), {
      params: this.params({
        invoiceNumber: model.invoiceNumber,
        search: model.search,
        status: model.status,
        employeeId: model.employeeId,
        customerId: model.customerId,
        dateFrom: model.dateFrom,
        dateTo: model.dateTo,
        productId: model.productId,
        states: model.states,
        stateId: model.stateId,
        countryId: model.countryId,
        cityId: model.cityId,
        pageIndex: model.pageIndex,
        pageSize: model.pageSize,
      }),
    });
  }

  getOrders(model: OrderViewModel): Observable<OrdersResult> {
    return this.http.get<OrdersResult>(this.url(), {
      params: this.params({
        invoiceNumber: model.invoiceNumber,
        search: model.search,
        status: model.status,
        employeeId: model.employeeId,
        customerId: model.customerId,
        dateFrom: model.dateFrom,
        dateTo: model.dateTo,
        productId: model.productId,
        states: model.states,
        stateId: model.stateId,
        countryId: model.countryId,
        cityId: model.cityId,
        pageIndex: model.pageIndex,
        pageSize: model.pageSize,
      }),
    });
  }

  getOrder(id: number): Observable<OrderDto> {
    return this.http.get<OrderDto>(this.url(`/${id}`));
  }

  createOrder(dto: CreateOrderDto): Observable<CreateOrderDto> {
    return this.http.post<CreateOrderDto>(this.url(), dto);
  }

  getAddProduct(id: number): Observable<SelectListItem[]> {
    return this.http.get<SelectListItem[]>(this.url(`/AddProduct/${id}`));
  }

  addProduct(model: UpdateOrderDto): Observable<ActionResponse> {
    return this.http.post<ActionResponse>(this.url('/AddProduct'), model);
  }

  updateOrder(id: number, dto: UpdateOrderDto): Observable<UpdateOrderDto> {
    return this.http.put<UpdateOrderDto>(this.url(`/${id}`), dto);
  }

  editStatus(id?: number): Observable<UpdateOrderDto> {
    return this.http.get<UpdateOrderDto>(this.url('/EditStatus'), {
      params: this.params({ id }),
    });
  }

  getUploadView(): Observable<void> {
    return this.http.get<void>(this.url('/UploadView'));
  }

  upload(file: File): Observable<boolean> {
    const form = new FormData();
    form.append('file', file);
    return this.http.post<boolean>(this.url('/Upload'), form);
  }

  exportPdf(id: number): Observable<Blob> {
    return this.http.get(this.url(`/ExportPdf/${id}`), { responseType: 'blob' });
  }

  changeStatus(id: number, status: number): Observable<void> {
    return this.http.post<void>(this.url('/ChangeStatus'), null, {
      params: this.params({ id, status }),
    });
  }

  updateArea(model: UpdateAreaDto): Observable<ActionResponse> {
    return this.http.post<ActionResponse>(this.url('/UpdateArea'), model);
  }

  updateStatus(model: UpdateStatusDto): Observable<ActionResponse> {
    return this.http.post<ActionResponse>(this.url('/UpdateStatus'), model);
  }

  getAssignArea(orderId?: number): Observable<AssignAreaResult> {
    return this.http.get<AssignAreaResult>(this.url('/AssignArea'), {
      params: this.params({ orderId }),
    });
  }

  assignArea(model: UpdateAreaDto): Observable<UpdateAreaDto> {
    return this.http.post<UpdateAreaDto>(this.url('/AssignArea'), model);
  }

  getCitiesByStateId(stateId: number): Observable<CityOption[]> {
    return this.http.get<CityOption[]>(this.url('/GetCitiesByStateId'), {
      params: this.params({ stateId }),
    });
  }

  getUsedAssignedLocations(): Observable<UsedAssignedLocationViewModel[]> {
    return this.http.get<UsedAssignedLocationViewModel[]>(this.url('/UsedAssignedLocations'));
  }

  getUsedAssignedLocationDetails(id: number): Observable<UsedAssignedLocationViewModel> {
    return this.http.get<UsedAssignedLocationViewModel>(this.url(`/UsedAssignedLocationDetails/${id}`));
  }

  createUsedAssignedLocation(model: UsedAssignedLocationViewModel): Observable<UsedAssignedLocationViewModel> {
    return this.http.post<UsedAssignedLocationViewModel>(this.url('/CreateUsedAssignedLocation'), model);
  }

  updateUsedAssignedLocation(id: number, model: UsedAssignedLocationViewModel): Observable<UsedAssignedLocationViewModel> {
    return this.http.put<UsedAssignedLocationViewModel>(this.url(`/UsedAssignedLocation/${id}`), model);
  }

  deleteUsedAssignedLocation(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/UsedAssignedLocation/${id}`));
  }

  getStatistics(): Observable<StatisticsResult> {
    return this.http.get<StatisticsResult>(this.url('/Statistics'));
  }

  statistics(model: StatisticsViewModel): Observable<StatisticsResult> {
    return this.http.post<StatisticsResult>(this.url('/Statistics'), model);
  }
}