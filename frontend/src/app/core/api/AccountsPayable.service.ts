import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Invoice {
  id: number;
  vendorId: number;
  invoiceNumber: string;
  invoiceDate: string;
  dueDate: string;
  amount: number;
  paidAmount: number;
  status: number;
  vendorName?: string;
}

@Injectable({ providedIn: 'root' })
export class AccountsPayableService extends ApiClientBase {
  protected readonly endpoint = 'AccountsPayable';

  constructor(http: HttpClient) {
    super(http);
  }

  getInvoices(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.url());
  }

  getInvoice(id: number): Observable<Invoice> {
    return this.http.get<Invoice>(this.url(`/${id}`));
  }

  createInvoice(dto: Invoice): Observable<Invoice> {
    return this.http.post<Invoice>(this.url(), dto);
  }

  updateInvoice(id: number, dto: Invoice): Observable<Invoice> {
    return this.http.put<Invoice>(this.url(`/${id}`), dto);
  }

  deleteInvoice(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
