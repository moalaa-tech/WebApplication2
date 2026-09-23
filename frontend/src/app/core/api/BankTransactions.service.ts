import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface BankTransaction {
  id: number;
  bankAccountId: number;
  transactionDate: string;
  referenceNumber: string;
  description: string;
  amount: number;
  runningBalance: number;
  type: number;
  status: number;
}

export interface CreateBankTransaction {
  bankAccountId: number;
  transactionDate: string;
  referenceNumber: string;
  description: string;
  amount: number;
  type: number;
  status: number;
}

@Injectable({ providedIn: 'root' })
export class BankTransactionsService extends ApiClientBase {
  protected readonly endpoint = 'BankTransactions';

  constructor(http: HttpClient) {
    super(http);
  }

  getBankTransactions(): Observable<BankTransaction[]> {
    return this.http.get<BankTransaction[]>(this.url());
  }

  filterBankTransactions(from?: string | null, to?: string | null, type?: number | null): Observable<BankTransaction[]> {
    return this.http.get<BankTransaction[]>(this.url('/Filter'), {
      params: this.params({ from, to, type }),
    });
  }

  createBankTransaction(dto: CreateBankTransaction): Observable<CreateBankTransaction> {
    return this.http.post<CreateBankTransaction>(this.url(), dto);
  }

  deleteBankTransaction(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
