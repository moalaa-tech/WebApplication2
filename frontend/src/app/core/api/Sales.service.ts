import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Lead {
  id: number;
  email: string;
  title: string;
  companyName: string;
  contactPerson: string;
  phone: string;
  status: number;
  assignedToUserId: string;
  assignedToUserName: string;
  firstName?: string;
  lastName?: string;
  totalScore?: string;
}

export interface Opportunity {
  id: number;
  leadId: number;
  lead?: Lead;
  title: string;
  stage: number;
  estimatedValue: number;
  createdAt: string;
  ownerUserName?: string;
  stageName?: string;
}

export interface CreateOpportunity {
  id?: number;
  leadId: number;
  lead?: Lead;
  title: string;
  stage: number;
  estimatedValue: number;
  createdAt: string;
}

export interface UpdateOpportunity {
  id: number;
  leadId: number;
  lead?: Lead;
  title: string;
  stage: number;
  estimatedValue: number;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class SalesService extends ApiClientBase {
  protected readonly endpoint = 'Sales';

  constructor(http: HttpClient) {
    super(http);
  }

  getSales(): Observable<Lead[]> {
    return this.http.get<Lead[]>(this.url());
  }

  getSale(id: number): Observable<Lead> {
    return this.http.get<Lead>(this.url(`/${id}`));
  }

  createSale(lead: Lead): Observable<Lead> {
    return this.http.post<Lead>(this.url(), lead);
  }

  updateSale(id: number, lead: Lead): Observable<Lead> {
    return this.http.put<Lead>(this.url(`/${id}`), lead);
  }

  deleteSale(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  convertToOpportunity(id: number): Observable<void> {
    return this.http.post<void>(this.url('/ConvertToOpportunity'), null, {
      params: this.params({ id }),
    });
  }

  getOpportunities(): Observable<Opportunity[]> {
    return this.http.get<Opportunity[]>(this.url('/Opportunities'));
  }

  getOpportunity(id?: number): Observable<Opportunity> {
    return this.http.get<Opportunity>(this.url('/Opportuniy'), {
      params: this.params({ id }),
    });
  }

  createOpportunity(dto: CreateOpportunity): Observable<Opportunity> {
    return this.http.get<Opportunity>(this.url('/AddOpportunity'), {
      params: this.params({
        id: dto.id,
        leadId: dto.leadId,
        title: dto.title,
        stage: dto.stage,
        estimatedValue: dto.estimatedValue,
        createdAt: dto.createdAt,
      }),
    });
  }

  updateOpportunity(dto: UpdateOpportunity): Observable<Opportunity> {
    return this.http.get<Opportunity>(this.url('/UpdateOpportunity'), {
      params: this.params({
        id: dto.id,
        leadId: dto.leadId,
        title: dto.title,
        stage: dto.stage,
        estimatedValue: dto.estimatedValue,
        createdAt: dto.createdAt,
      }),
    });
  }

  addScore(leadId: number, criteriaId: number, customPoints?: number): Observable<void> {
    return this.http.post<void>(this.url('/AddScore'), null, {
      params: this.params({ leadId, criteriaId, customPoints }),
    });
  }
}