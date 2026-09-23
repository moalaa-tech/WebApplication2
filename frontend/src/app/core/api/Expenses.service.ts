import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Expense {
  id: number;
  title: string;
  titleAR: string;
  amount: number;
  date: string;
  categoryId?: number | null;
  categoryName?: string | null;
  notes?: string | null;
}

export interface SelectListItem {
  text: string;
  value: string;
  selected?: boolean;
  disabled?: boolean;
}

export interface CreateEditExpense {
  id: number;
  title: string;
  titleAR: string;
  notes?: string | null;
  amount: number;
  date: string;
  categoryId?: number | null;
  categories?: SelectListItem[] | null;
}

export interface ExpenseQuery {
  page?: number;
  pageSize?: number;
  categoryId?: number | null;
  from?: string | null;
  to?: string | null;
  search?: string | null;
}

export interface ExpenseList {
  items: Expense[];
  page: number;
  pageSize: number;
  totalCount: number;
  from?: string | null;
  to?: string | null;
  categoryId?: number | null;
  categories?: SelectListItem[] | null;
}

export interface MonthlyReport {
  month: number;
  total: number;
}

export interface ExpensesDashboard {
  year: number;
  monthlyTotals: number[];
}

@Injectable({ providedIn: 'root' })
export class ExpensesService extends ApiClientBase {
  protected readonly endpoint = 'Expenses';

  constructor(http: HttpClient) {
    super(http);
  }

  getExpenses(query?: ExpenseQuery): Observable<ExpenseList> {
    return this.http.get<ExpenseList>(this.url(), {
      params: this.params({
        page: query?.page,
        pageSize: query?.pageSize,
        categoryId: query?.categoryId,
        from: query?.from,
        to: query?.to,
        search: query?.search,
      }),
    });
  }

  getExpense(id: number): Observable<Expense | null> {
    return this.http.get<Expense | null>(this.url(`/${id}`));
  }

  createExpense(dto: CreateEditExpense): Observable<Expense> {
    return this.http.post<Expense>(this.url(), dto);
  }

  updateExpense(id: number, dto: CreateEditExpense): Observable<CreateEditExpense> {
    return this.http.put<CreateEditExpense>(this.url(`/${id}`), dto);
  }

  deleteExpense(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getMonthlyReport(year?: number): Observable<MonthlyReport[]> {
    return this.http.get<MonthlyReport[]>(this.url('/MonthlyReport'), {
      params: this.params({ year }),
    });
  }

  getExpensesDashboard(year?: number): Observable<ExpensesDashboard> {
    return this.http.get<ExpensesDashboard>(this.url('/ExpensesDashboard'), {
      params: this.params({ year }),
    });
  }
}
