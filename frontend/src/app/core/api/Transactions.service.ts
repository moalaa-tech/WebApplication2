import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

@Injectable({ providedIn: 'root' })
export class TransactionsService extends ApiClientBase {
  protected readonly endpoint = 'Transactions';

  constructor(http: HttpClient) {
    super(http);
  }

  getTransactions(productId?: string): Observable<void> {
    return this.http.get<void>(this.url(), {
      params: this.params({ productId }),
    });
  }
}