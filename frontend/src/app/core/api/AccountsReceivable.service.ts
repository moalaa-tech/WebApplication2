import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface AccountsReceivable {
  id: number;
  customerName: string;
  invoiceNumber: string;
  invoiceDate: string;
  amountDue: number;
  dueDate: string;
}

export interface CreateAccountsReceivable {
  customerName: string;
  invoiceNumber: string;
  invoiceDate: string;
  amountDue: number;
  dueDate: string;
}

@Injectable({ providedIn: 'root' })
export class AccountsReceivableService extends ApiClientBase {
  protected readonly endpoint = 'AccountsReceivable';

  constructor(http: HttpClient) {
    super(http);
  }

  getAccountsReceivables(): Observable<AccountsReceivable[]> {
    return this.http.get<AccountsReceivable[]>(this.url());
  }

  getAccountsReceivable(id: number): Observable<AccountsReceivable> {
    return this.http.get<AccountsReceivable>(this.url(`/${id}`));
  }

  createAccountsReceivable(dto: CreateAccountsReceivable): Observable<CreateAccountsReceivable> {
    return this.http.post<CreateAccountsReceivable>(this.url(), dto);
  }

  updateAccountsReceivable(id: number, dto: AccountsReceivable): Observable<AccountsReceivable> {
    return this.http.put<AccountsReceivable>(this.url(`/${id}`), dto);
  }

  deleteAccountsReceivable(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
