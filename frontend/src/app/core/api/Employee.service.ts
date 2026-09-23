import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Department {
  id: number;
  name: string;
  nameAr: string;
  description: string;
  location: string;
  budget: number;
  establishedDate: string;
  managerName: string;
  employeeCount: unknown;
  managerId?: number | null;
  manager?: Employee | null;
  isActive: boolean;
}

export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  name: string;
  description: string;
  departmentId?: number | null;
  department?: Department | null;
}

export interface CreateEmployee {
  firstName: string;
  lastName: string;
  description: string;
  departmentId?: number | null;
  gender: string;
}

export interface UpdateEmployee {
  id: number;
  firstName: string;
  lastName: string;
  description: string;
  departmentId?: number | null;
  genderId: number;
}

@Injectable({ providedIn: 'root' })
export class EmployeeService extends ApiClientBase {
  protected readonly endpoint = 'Employee';

  constructor(http: HttpClient) {
    super(http);
  }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url());
  }

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<Employee>(this.url(`/${id}`));
  }

  createEmployee(dto: CreateEmployee): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateEmployee(id: number, dto: UpdateEmployee): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getOrganizationChart(): Observable<unknown[][]> {
    return this.http.get<unknown[][]>(this.url('/OrganizationChart'));
  }
}