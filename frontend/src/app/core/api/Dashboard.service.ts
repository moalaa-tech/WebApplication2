import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

@Injectable({ providedIn: 'root' })
export class DashboardService extends ApiClientBase {
  protected readonly endpoint = 'Dashboard';

  constructor(http: HttpClient) {
    super(http);
  }

  getDashboard(): Observable<void> {
    return this.http.get<void>(this.url());
  }
}
