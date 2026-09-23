import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface BankAccount {
  id: number;
  accountNumber: string;
  bankName: string;
  accountName: string;
  currency: string;
  currentBalance: number;
}

export interface CreateBankAccount {
  accountNumber: string;
  bankName: string;
  accountName: string;
  currency: string;
}

export interface UpdateBankAccount extends CreateBankAccount {
  id: number;
}

@Injectable({ providedIn: 'root' })
export class BankAccountsService extends ApiClientBase {
  protected readonly endpoint = 'BankAccounts';

  constructor(http: HttpClient) {
    super(http);
  }

  getBankAccounts(): Observable<BankAccount[]> {
    return this.http.get<BankAccount[]>(this.url());
  }

  getBankAccount(id: number): Observable<BankAccount> {
    return this.http.get<BankAccount>(this.url(`/${id}`));
  }

  createBankAccount(dto: CreateBankAccount): Observable<CreateBankAccount> {
    return this.http.post<CreateBankAccount>(this.url(), dto);
  }

  updateBankAccount(id: number, dto: UpdateBankAccount): Observable<UpdateBankAccount> {
    return this.http.put<UpdateBankAccount>(this.url(`/${id}`), dto);
  }

  deleteBankAccount(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
