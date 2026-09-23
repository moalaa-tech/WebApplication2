import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Company {
  id: number;
  name: string;
  nameAr: string;
  businessId: number;
  address: string;
  city: string;
  userId: number;
  creationDate: string;
  email: string;
}

export interface CreateCompany {
  name: string;
  nameAr: string;
  businessId: number;
  address: string;
  city: string;
  cityId: number;
  userId: number;
}

export interface UpdateCompany {
  id: number;
  name: string;
  nameAr: string;
  businessId: number;
  address: string;
  city: string;
  cityId: number;
  userId: number;
  creationDate: string;
  isDeleted: number;
}

@Injectable({ providedIn: 'root' })
export class CompaniesService extends ApiClientBase {
  protected readonly endpoint = 'Companies';

  constructor(http: HttpClient) {
    super(http);
  }

  getCompanies(): Observable<Company[]> {
    return this.http.get<Company[]>(this.url());
  }

  getCompany(id: number): Observable<Company> {
    return this.http.get<Company>(this.url(`/${id}`));
  }

  createCompany(dto: CreateCompany): Observable<Company> {
    return this.http.post<Company>(this.url(), dto);
  }

  updateCompany(id: number, dto: UpdateCompany): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteCompany(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
