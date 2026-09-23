import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ServiceRequest {
  id: number;
  requestType: string;
  description: string;
  status: number;
  customerId: number;
  createdDate: string;
}

export interface CreateServiceRequest {
  requestType: string;
  description: string;
  customerId: number;
}

export interface ServiceRequestEdit {
  id: number;
  requestType: string;
  description: string;
  status: string;
  customerId: number;
  createdDate: string;
  resolutionNotes: string;
}

@Injectable({ providedIn: 'root' })
export class ServiceRequestService extends ApiClientBase {
  protected readonly endpoint = 'ServiceRequest';

  constructor(http: HttpClient) {
    super(http);
  }

  getServiceRequests(): Observable<ServiceRequest[]> {
    return this.http.get<ServiceRequest[]>(this.url());
  }

  createServiceRequest(dto: CreateServiceRequest): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateServiceRequest(id: number, dto: ServiceRequestEdit): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteServiceRequest(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getServiceRequest(id: number): Observable<ServiceRequest> {
    return this.http.get<ServiceRequest>(this.url(`/${id}`));
  }
}
