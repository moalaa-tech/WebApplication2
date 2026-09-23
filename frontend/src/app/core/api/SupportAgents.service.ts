import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface SupportAgent {
  id: number;
  fullName: string;
  email: string;
  department: string;
  jobTitle: string;
  isActive: boolean;
  openTickets: number;
  averageRating: number;
}

export interface SupportAgentTicket {
  id: number;
  ticketId: number;
  ticketNumber: string;
  subject: string;
  status: string;
  createdDate: string;
  resolutionDate?: string | null;
  priority: string;
}

export interface SupportAgentDetail {
  id: number;
  fullName: string;
  email: string;
  phoneNumber: string;
  jobTitle: string;
  department: string;
  specialization: string;
  skills: string[];
  workingHours: string;
  hireDate: string;
  isActive: boolean;
  totalTicketsResolved: number;
  averageRating: number;
  averageResolutionTime: number;
  recentTickets: SupportAgentTicket[];
}

export interface SupportAgentCreate {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  jobTitle: string;
  department: string;
  specialization: string;
  workingHours: string;
  timeZone: string;
  skills: string;
}

export interface SupportAgentEdit extends SupportAgentCreate {
  id: number;
  isActive: boolean;
}

@Injectable({ providedIn: 'root' })
export class SupportAgentsService extends ApiClientBase {
  protected readonly endpoint = 'SupportAgents';

  constructor(http: HttpClient) {
    super(http);
  }

  getSupportAgents(): Observable<SupportAgent[]> {
    return this.http.get<SupportAgent[]>(this.url());
  }

  getSupportAgent(id: number): Observable<SupportAgentDetail> {
    return this.http.get<SupportAgentDetail>(this.url(`/${id}`));
  }

  createSupportAgent(dto: SupportAgentCreate): Observable<number> {
    return this.http.post<number>(this.url(), dto);
  }

  updateSupportAgent(id: number, dto: SupportAgentEdit): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  toggleStatus(id?: number, isActive?: boolean): Observable<void> {
    return this.http.post<void>(this.url('/ToggleStatus'), null, { params: this.params({ id, isActive }) });
  }
}
