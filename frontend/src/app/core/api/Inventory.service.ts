import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

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

export interface ReorderSuggestionDto {
  productId: number;
  productName: string;
  sku: string;
  currentStock: number;
  minimumStockLevel: number;
  suggestedOrderQuantity: number;
}

export interface BatchDto {
  id: number;
  batchNumber: string;
  manufactureDate?: string | null;
  expiryDate?: string | null;
  productId: number;
  warehouseName: string;
  warehouseId: number;
  initialQuantity: number;
  quantityOnHand: number;
}

export interface SelectListItem {
  value: string;
  text: string;
  selected?: boolean;
}

export interface ScanBarcodeResult {
  id: number;
}

export interface ReceiveGoodsViewModel {
  itemId: number;
  warehouseId: number;
  quantity: number;
  reference: string;
  batchNumber?: string | null;
  manufactureDate?: string | null;
  expiryDate?: string | null;
  barcode?: string | null;
  items?: SelectListItem[];
  warehouses?: SelectListItem[];
}

export interface IssueGoodsViewModel {
  itemId: number;
  warehouseId: number;
  quantity: number;
  reference: string;
  batchId?: number | null;
  barcode?: string | null;
}

export interface EditProductViewModel {
  id: number;
  name: string;
  nameAr?: string | null;
  totalCost: number;
  shippingCost: number;
  freightCost: number;
  productTypeId?: number | null;
  existingImageFile?: string | null;
  newImageFile?: unknown;
  description?: string | null;
  productTypes?: SelectListItem[];
}

export interface CreateProductViewModel {
  name: string;
  nameAr?: string | null;
  totalCost: number;
  shippingCost: number;
  freightCost: number;
  productTypeId?: number | null;
  imageFileUpload?: unknown;
  imageFile?: string | null;
  description?: string | null;
  productTypes?: SelectListItem[];
}

export interface ProductDetailsViewModel {
  product: ProductDto;
  batches: BatchDto[];
}

@Injectable({ providedIn: 'root' })
export class InventoryService extends ApiClientBase {
  protected readonly endpoint = 'Inventory';

  constructor(http: HttpClient) {
    super(http);
  }

  getDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/dashboard'));
  }

  getInventorys(pageIndex?: number): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(this.url(), {
      params: this.params({ pageIndex }),
    });
  }

  getReorderSuggestions(): Observable<ReorderSuggestionDto[]> {
    return this.http.get<ReorderSuggestionDto[]>(this.url('/ReorderSuggestions'));
  }

  getBatchDetails(itemId: number, warehouseId?: number): Observable<BatchDto[]> {
    return this.http.get<BatchDto[]>(this.url('/BatchDetails'), {
      params: this.params({ itemId, warehouseId }),
    });
  }

  getScanBarcode(): Observable<void> {
    return this.http.get<void>(this.url('/ScanBarcode'));
  }

  scanBarcode(barcode?: string): Observable<ScanBarcodeResult> {
    return this.http.post<ScanBarcodeResult>(this.url('/ScanBarcode'), null, {
      params: this.params({ barcode }),
    });
  }

  editProduct(model: EditProductViewModel): Observable<void> {
    return this.http.put<void>(this.url('/Edit'), model);
  }

  createItem(vm: CreateProductViewModel): Observable<ProductDto> {
    return this.http.post<ProductDto>(this.url('/CreateItem'), vm);
  }

  getReceiveGoods(): Observable<ReceiveGoodsViewModel> {
    return this.http.get<ReceiveGoodsViewModel>(this.url('/ReceiveGoods'));
  }

  receiveGoods(vm: ReceiveGoodsViewModel): Observable<ReceiveGoodsViewModel> {
    return this.http.post<ReceiveGoodsViewModel>(this.url('/ReceiveGoods'), vm);
  }

  getIssueGoods(): Observable<IssueGoodsViewModel> {
    return this.http.get<IssueGoodsViewModel>(this.url('/IssueGoods'));
  }

  issueGoods(vm: IssueGoodsViewModel): Observable<IssueGoodsViewModel> {
    return this.http.post<IssueGoodsViewModel>(this.url('/IssueGoods'), vm);
  }

  getInventory(id: number): Observable<ProductDetailsViewModel> {
    return this.http.get<ProductDetailsViewModel>(this.url(`/${id}`));
  }

  lookupByBarcode(code: string): Observable<ProductDto> {
    return this.http.get<ProductDto>(this.url('/LookupByBarcode'), {
      params: this.params({ code }),
    });
  }
}