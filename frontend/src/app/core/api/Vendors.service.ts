import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Vendor {
  id: number;
  name: string;
  contactPerson: string;
  email: string;
  phone: string;
  address?: string | null;
}

@Injectable({ providedIn: 'root' })
export class VendorsService extends ApiClientBase {
  protected readonly endpoint = 'Vendors';

  constructor(http: HttpClient) {
    super(http);
  }

  getVendors(search?: string): Observable<Vendor[]> {
    return this.http.get<Vendor[]>(this.url(), { params: this.params({ search }) });
  }

  getVendor(id: number): Observable<Vendor> {
    return this.http.get<Vendor>(this.url(`/${id}`));
  }

  createVendor(dto: Vendor): Observable<Vendor> {
    return this.http.post<Vendor>(this.url(), dto);
  }

  updateVendor(id: number, dto: Vendor): Observable<Vendor> {
    return this.http.put<Vendor>(this.url(`/${id}`), dto);
  }

  deleteVendor(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}