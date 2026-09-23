import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Customer {
  id: number;
  name: string | null;
  nameAr: string | null;
  description: string | null;
  email: string | null;
  address: string | null;
  phone: string | null;
}

export interface CreateCustomer {
  name: string;
  nameAr?: string | null;
  description?: string | null;
  email?: string | null;
  address?: string | null;
  phone: string;
}

export interface UpdateCustomer {
  id: number;
  name: string;
  nameAr?: string | null;
  description?: string | null;
  email?: string | null;
  address?: string | null;
  phone: string;
}

@Injectable({ providedIn: 'root' })
export class CustomerService extends ApiClientBase {
  protected readonly endpoint = 'Customer';

  constructor(http: HttpClient) {
    super(http);
  }

  getCustomers(page?: number): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.url(), { params: this.params({ page }) });
  }

  getCustomer(id: number): Observable<Customer> {
    return this.http.get<Customer>(this.url(`/${id}`));
  }

  createCustomer(dto: CreateCustomer): Observable<CreateCustomer> {
    return this.http.post<CreateCustomer>(this.url(), dto);
  }

  updateCustomer(id: number, dto: UpdateCustomer): Observable<UpdateCustomer> {
    return this.http.put<UpdateCustomer>(this.url(`/${id}`), dto);
  }

  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}