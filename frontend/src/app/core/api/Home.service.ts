import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ErrorViewModel {
  requestId?: string | null;
  showRequestId: boolean;
}

@Injectable({ providedIn: 'root' })
export class HomeService extends ApiClientBase {
  protected readonly endpoint = 'Home';

  constructor(http: HttpClient) {
    super(http);
  }

  getHome(): Observable<void> {
    return this.http.get<void>(this.url());
  }

  getLanding(): Observable<void> {
    return this.http.get<void>(this.url('/landing'));
  }

  getPrivacy(): Observable<void> {
    return this.http.get<void>(this.url('/Privacy'));
  }

  getShowServerToaster(): Observable<void> {
    return this.http.get<void>(this.url('/ShowServerToaster'));
  }

  setLanguage(culture?: string, returnUrl?: string): Observable<void> {
    return this.http.post<void>(this.url('/SetLanguage'), null, {
      params: this.params({ culture, returnUrl }),
    });
  }

  getError(): Observable<ErrorViewModel> {
    return this.http.get<ErrorViewModel>(this.url('/Error'));
  }

  getNotFound(code?: number): Observable<void> {
    return this.http.get<void>(this.url('/NotFound'), { params: this.params({ code }) });
  }

  getProcurementManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/ProcurementManagementDashboard'));
  }

  getCustomersManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/CustomersManagementDashboard'));
  }

  getSalesManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/SalesManagementDashboard'));
  }

  getEmployeeManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/EmployeeManagementDashboard'));
  }

  getFinanceManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/FinanceManagementDashboard'));
  }

  getGeneralAccountingManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/GeneralAccountingManagementDashboard'));
  }

  getTemplatesManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/TemplatesManagementDashboard'));
  }

  getInventoryDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/InventoryDashboard'));
  }

  getSettingsManagementDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/SettingsManagementDashboard'));
  }

  getRomaDashboard(): Observable<void> {
    return this.http.get<void>(this.url('/RomaDashboard'));
  }
}
