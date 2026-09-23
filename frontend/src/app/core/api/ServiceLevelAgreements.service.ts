import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface SLAMetrics {
  complianceRate: number;
  totalBreaches: number;
  totalCases: number;
  averageResolutionTime: number;
  averageResponseTime: number;
}

export interface ServiceLevelAgreement {
  id: number;
  name: string;
  description: string;
  serviceType: string;
  responseTime: number;
  resolutionTime: number;
  createdDate: string;
  lastModified?: string | null;
  escalationProcess: string;
  termsAndConditions: string;
  metrics: SLAMetrics;
}

export interface CreateServiceLevelAgreement {
  name: string;
  description: string;
  serviceType: string;
  responseTime: number;
  resolutionTime: number;
  escalationProcess: string;
  termsAndConditions: string;
}

export interface EditServiceLevelAgreement {
  id: number;
  name: string;
  description: string;
  serviceType: string;
  responseTime: number;
  resolutionTime: number;
  escalationProcess: string;
  termsAndConditions: string;
}

@Injectable({ providedIn: 'root' })
export class ServiceLevelAgreementsService extends ApiClientBase {
  protected readonly endpoint = 'ServiceLevelAgreements';

  constructor(http: HttpClient) {
    super(http);
  }

  getServiceLevelAgreements(): Observable<ServiceLevelAgreement[]> {
    return this.http.get<ServiceLevelAgreement[]>(this.url());
  }

  createServiceLevelAgreement(dto: CreateServiceLevelAgreement): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateServiceLevelAgreement(id: number, dto: EditServiceLevelAgreement): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  getServiceLevelAgreement(id: number): Observable<ServiceLevelAgreement> {
    return this.http.get<ServiceLevelAgreement>(this.url(`/${id}`));
  }
}
