import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface SupplierDropdown {
  id: number;
  name: string;
  code: string;
  isActive: boolean;
  displayText: string;
}

export interface PurchaseOrder {
  id: number;
  poNumber: string;
  orderDate: string;
  expectedDeliveryDate?: string | null;
  supplierId: number;
  supplierName?: string;
  supplierCode?: string;
  description?: string;
  totalAmount: number;
  status: string;
  createdDate?: string;
  modifiedDate?: string | null;
  items?: PurchaseOrderItem[];
  availableSuppliers?: SupplierDropdown[];
  availableStatuses?: string[];
}

export interface PurchaseOrderItem {
  id: number;
  purchaseOrderId: number;
  itemName: string;
  itemCode?: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  description?: string;
}

export interface CreatePurchaseOrderItem {
  itemName: string;
  itemCode?: string;
  quantity: number;
  unitPrice: number;
  description?: string;
}

export interface CreatePurchaseOrder {
  poNumber: string;
  orderDate: string;
  expectedDeliveryDate?: string | null;
  supplierId: number;
  description?: string;
  items?: CreatePurchaseOrderItem[];
  availableSuppliers?: SupplierDropdown[];
}

export interface EditPurchaseOrder {
  id: number;
  poNumber: string;
  orderDate: string;
  expectedDeliveryDate?: string | null;
  supplierId: number;
  description?: string;
  items?: CreatePurchaseOrderItem[];
  availableSuppliers?: SupplierDropdown[];
  status?: string;
  totalAmount?: number;
}

export interface MessageResult {
  message: string;
}

@Injectable({ providedIn: 'root' })
export class PurchaseOrdersService extends ApiClientBase {
  protected readonly endpoint = 'PurchaseOrders';

  constructor(http: HttpClient) {
    super(http);
  }

  getPurchaseOrders(): Observable<PurchaseOrder[]> {
    return this.http.get<PurchaseOrder[]>(this.url());
  }

  getPurchaseOrder(id: number): Observable<PurchaseOrder> {
    return this.http.get<PurchaseOrder>(this.url(`/${id}`));
  }

  createPurchaseOrder(model: CreatePurchaseOrder): Observable<CreatePurchaseOrder> {
    return this.http.post<CreatePurchaseOrder>(this.url(), model);
  }

  updatePurchaseOrder(id: number, model: EditPurchaseOrder): Observable<EditPurchaseOrder> {
    return this.http.put<EditPurchaseOrder>(this.url(`/${id}`), model);
  }

  deletePurchaseOrder(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  changeStatus(id: number, status: string): Observable<MessageResult> {
    return this.http.post<MessageResult>(this.url('/ChangeStatus'), null, {
      params: this.params({ id, status }),
    });
  }

  verifyPONumber(poNumber: string, id?: number): Observable<boolean> {
    return this.http.get<boolean>(this.url('/VerifyPONumber'), {
      params: this.params({ poNumber, id }),
    });
  }
}