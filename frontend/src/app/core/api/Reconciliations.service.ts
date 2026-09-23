import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Reconciliation {
  id: number;
  bankAccountId: number;
  statementDate: string;
  statementBalance: number;
  adjustedBookBalance: number;
  isReconciled: boolean;
}

export interface CreateReconciliation {
  bankAccountId: number;
  statementDate: string;
  statementBalance: number;
  adjustedBookBalance: number;
  isReconciled: boolean;
}

export interface ReconciliationItem {
  id: number;
  reconciliationId: number;
  bankTransactionId: number;
  status: number;
  adjustedAmount: number;
  notes: string;
}

export interface ReconciliationDetails {
  reconciliation: Reconciliation;
  items: ReconciliationItem[];
}

@Injectable({ providedIn: 'root' })
export class ReconciliationsService extends ApiClientBase {
  protected readonly endpoint = 'Reconciliations';

  constructor(http: HttpClient) {
    super(http);
  }

  getReconciliations(): Observable<Reconciliation[]> {
    return this.http.get<Reconciliation[]>(this.url());
  }

  createReconciliation(dto: CreateReconciliation): Observable<CreateReconciliation> {
    return this.http.post<CreateReconciliation>(this.url(), dto);
  }

  getReconciliation(id: number): Observable<ReconciliationDetails> {
    return this.http.get<ReconciliationDetails>(this.url(`/${id}`));
  }
}
