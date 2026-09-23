import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ExpenseCategory {
  id: number;
  name: string;
  nameAR: string;
}

export interface ExpenseCategoryCreate {
  name: string;
  nameAR: string;
}

export interface ExpenseCategoryUpdate {
  id: number;
  name: string;
  nameAR: string;
}

@Injectable({ providedIn: 'root' })
export class ExpenseCategoriesService extends ApiClientBase {
  protected readonly endpoint = 'ExpenseCategories';

  constructor(http: HttpClient) {
    super(http);
  }

  getExpenseCategories(): Observable<ExpenseCategory[]> {
    return this.http.get<ExpenseCategory[]>(this.url());
  }

  getExpenseCategory(id: number): Observable<ExpenseCategory> {
    return this.http.get<ExpenseCategory>(this.url(`/${id}`));
  }

  createExpenseCategory(dto: ExpenseCategoryCreate): Observable<ExpenseCategoryCreate> {
    return this.http.post<ExpenseCategoryCreate>(this.url(), dto);
  }

  updateExpenseCategory(id: number, dto: ExpenseCategoryUpdate): Observable<ExpenseCategoryUpdate> {
    return this.http.put<ExpenseCategoryUpdate>(this.url(`/${id}`), dto);
  }

  deleteExpenseCategory(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
