import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface HealthEntry {
  name: string;
  status: string;
  description?: string | null;
  duration: string;
  data?: Record<string, unknown> | null;
  exception?: string | null;
}

export interface HealthReport {
  status: string;
  totalDuration: string;
  entries: HealthEntry[];
}

export interface HealthLive {
  status: string;
  timestamp: string;
  error?: string;
}

export interface HealthReadyIssue {
  service: string;
  status: string;
}

export interface HealthReady {
  status: string;
  timestamp: string;
  issues?: HealthReadyIssue[];
  error?: string;
}

export interface ServiceHealth {
  name: string;
  status: string;
  description?: string | null;
  duration: string;
  data?: Record<string, unknown> | null;
  exception?: string | null;
  timestamp: string;
}

@Injectable({ providedIn: 'root' })
export class HealthService extends ApiClientBase {
  protected readonly endpoint = 'Health';

  constructor(http: HttpClient) {
    super(http);
  }

  getHealth(): Observable<HealthReport> {
    return this.http.get<HealthReport>(this.url());
  }

  getLive(): Observable<HealthLive> {
    return this.http.get<HealthLive>(this.url('/live'));
  }

  getReady(): Observable<HealthReady> {
    return this.http.get<HealthReady>(this.url('/ready'));
  }

  getServiceHealth(serviceName: string): Observable<ServiceHealth> {
    return this.http.get<ServiceHealth>(this.url(`/${encodeURIComponent(serviceName)}`));
  }
}
