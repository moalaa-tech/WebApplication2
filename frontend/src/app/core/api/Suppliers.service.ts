import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Supplier {
  id: number;
  name: string;
  code: string;
  address: string;
  contactPerson: string;
  phone: string;
  email: string;
  taxId: string;
  isActive: boolean;
  createdDate?: string;
}

export interface CreateSupplier {
  name: string;
  code: string;
  address: string;
  contactPerson: string;
  phone: string;
  email: string;
  taxId: string;
}

export interface EditSupplier {
  id: number;
  name: string;
  code: string;
  address: string;
  contactPerson: string;
  phone: string;
  email: string;
  taxId: string;
  isActive: boolean;
}

@Injectable({ providedIn: 'root' })
export class SuppliersService extends ApiClientBase {
  protected readonly endpoint = 'Suppliers';

  constructor(http: HttpClient) {
    super(http);
  }

  getSuppliers(): Observable<Supplier[]> {
    return this.http.get<Supplier[]>(this.url());
  }

  getSupplier(id: number): Observable<Supplier> {
    return this.http.get<Supplier>(this.url(`/${id}`));
  }

  createSupplier(model: CreateSupplier): Observable<CreateSupplier> {
    return this.http.post<CreateSupplier>(this.url(), model);
  }

  updateSupplier(id: number, model: EditSupplier): Observable<EditSupplier> {
    return this.http.put<EditSupplier>(this.url(`/${id}`), model);
  }

  deleteSupplier(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}