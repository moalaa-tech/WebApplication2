import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface GeneralLedger {
  id: number;
  transactionDate: string;
  accountNumber: string;
  description: string;
  debitAmount: number;
  creditAmount: number;
  reference: string;
  isPosted: boolean;
}

@Injectable({ providedIn: 'root' })
export class GeneralLedgerService extends ApiClientBase {
  protected readonly endpoint = 'GeneralLedger';

  constructor(http: HttpClient) {
    super(http);
  }

  getGeneralLedgers(): Observable<GeneralLedger[]> {
    return this.http.get<GeneralLedger[]>(this.url());
  }

  createGeneralLedger(dto: GeneralLedger): Observable<GeneralLedger> {
    return this.http.post<GeneralLedger>(this.url(), dto);
  }

  updateGeneralLedger(id: number, dto: GeneralLedger): Observable<GeneralLedger> {
    return this.http.put<GeneralLedger>(this.url(`/${id}`), dto);
  }

  deleteGeneralLedger(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  postTransaction(id: number): Observable<void> {
    return this.http.post<void>(this.url(`/PostTransaction/${id}`), null);
  }
}
