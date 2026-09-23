import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Customer {
  id: number;
  name?: string | null;
  nameAr?: string | null;
  description?: string | null;
  email?: string | null;
  address?: string | null;
  phone?: string | null;
}

export interface Ticket {
  id: number;
  subject: string;
  description: string;
  status: number;
  priority: number;
  customer: Customer;
  customerId: number;
  assignedTo?: number | null;
  createdDate: string;
  lastUpdated: string;
}

export interface CreateTicket {
  subject: string;
  description: string;
  status: number;
  priority: number;
  customerId: number;
  assignedTo?: number | null;
  createdDate: string;
  lastUpdated: string;
}

export interface UpdateTicket extends CreateTicket {
  id: number;
}

@Injectable({ providedIn: 'root' })
export class TicketsService extends ApiClientBase {
  protected readonly endpoint = 'Tickets';

  constructor(http: HttpClient) {
    super(http);
  }

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.url());
  }

  getTicket(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(this.url(`/${id}`));
  }

  createTicket(dto: CreateTicket): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateTicket(id: number, dto: UpdateTicket): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteTicket(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
