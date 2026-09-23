import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ReconciliationItem {
  id: number;
  reconciliationId: number;
  bankTransactionId: number;
  status: number;
  adjustedAmount: number;
  notes: string;
}

export interface EditReconciliationItem {
  id: number;
  adjustedAmount: number;
  status: number;
  notes: string;
}

@Injectable({ providedIn: 'root' })
export class ReconciliationItemsService extends ApiClientBase {
  protected readonly endpoint = 'ReconciliationItems';

  constructor(http: HttpClient) {
    super(http);
  }

  getReconciliationItems(reconciliationId: number): Observable<ReconciliationItem[]> {
    return this.http.get<ReconciliationItem[]>(this.url(), { params: this.params({ reconciliationId }) });
  }

  updateReconciliationItem(id: number, dto: EditReconciliationItem): Observable<EditReconciliationItem> {
    return this.http.put<EditReconciliationItem>(this.url(`/${id}`), dto);
  }

  deleteReconciliationItem(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
