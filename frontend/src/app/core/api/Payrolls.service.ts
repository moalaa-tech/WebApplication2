import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface PayrollDto {
  id: number;
  employeeId: number;
  employeeName: string;
  employeeNumber: string;
  basicSalary: number;
  allowances: number;
  deductions: number;
  taxAmount: number;
  netSalary: number;
  payPeriodStart: string;
  payPeriodEnd: string;
  paymentDate: string;
  paymentMethod: string;
  isProcessed: boolean;
}

export interface CreatePayrollDto {
  employeeId: number;
  employeeName: string;
  employeeNumber: string;
  basicSalary: number;
  allowances: number;
  deductions: number;
  taxAmount: number;
  netSalary: number;
  payPeriodStart: string;
  payPeriodEnd: string;
  paymentDate: string;
  paymentMethod: string;
  isProcessed: boolean;
}

@Injectable({ providedIn: 'root' })
export class PayrollsService extends ApiClientBase {
  protected readonly endpoint = 'Payrolls';

  constructor(http: HttpClient) {
    super(http);
  }

  getPayrolls(startDate: string, endDate: string): Observable<PayrollDto[]> {
    return this.http.get<PayrollDto[]>(this.url(), {
      params: this.params({ startDate, endDate }),
    });
  }

  processPayroll(dto: CreatePayrollDto): Observable<void> {
    return this.http.post<void>(this.url('/Process'), dto);
  }
}