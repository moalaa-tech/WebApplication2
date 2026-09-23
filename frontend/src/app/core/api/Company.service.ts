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

export interface CompanyViewModel {
  id: number;
  name: string;
  nameAr: string;
  businessId: number;
  businessName: string;
  address: string;
  city: string;
  userId: number;
  userName: string;
  creationDate: string;
  email: string;
}

export interface SelectListItem {
  text: string;
  value?: string | null;
  selected: boolean;
  disabled: boolean;
}

export interface CreateCompanyViewModel {
  name: string;
  nameAr: string;
  businessId: number;
  address: string;
  city: string;
  userId: number;
  businesses?: SelectListItem[];
  users?: SelectListItem[];
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
export class CompanyService extends ApiClientBase {
  protected readonly endpoint = 'Company';

  constructor(http: HttpClient) {
    super(http);
  }

  getCompanies(): Observable<CompanyViewModel[]> {
    return this.http.get<CompanyViewModel[]>(this.url());
  }

  getCompany(id: number): Observable<Company> {
    return this.http.get<Company>(this.url(`/${id}`));
  }

  createCompany(dto: CreateCompanyViewModel): Observable<CreateCompanyViewModel | null> {
    return this.http.post<CreateCompanyViewModel | null>(this.url(), dto);
  }

  updateCompany(id: number, dto: UpdateCompany): Observable<UpdateCompany | null> {
    return this.http.put<UpdateCompany | null>(this.url(`/${id}`), dto);
  }

  deleteCompany(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
