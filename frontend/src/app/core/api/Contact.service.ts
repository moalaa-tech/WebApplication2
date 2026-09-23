import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ContactViewModel {
  id: number;
  name: string;
  nameAr: string;
  surname: string;
  fullName: string;
  phone: string;
  email: string;
  position: string;
  companyId: number;
  companyName: string;
  lastContactDate?: string | null;
  status: number;
}

export interface CreateContact {
  name: string;
  surname: string;
  phone: string;
  email: string;
  position: string;
  companyId: number;
}

export interface UpdateContact extends CreateContact {
  id: number;
}

@Injectable({ providedIn: 'root' })
export class ContactService extends ApiClientBase {
  protected readonly endpoint = 'Contact';

  constructor(http: HttpClient) {
    super(http);
  }

  getContacts(): Observable<ContactViewModel[]> {
    return this.http.get<ContactViewModel[]>(this.url());
  }

  getContact(id: number): Observable<ContactViewModel> {
    return this.http.get<ContactViewModel>(this.url(`/${id}`));
  }

  createContact(dto: CreateContact): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateContact(id: number, dto: UpdateContact): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteContact(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
