import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  name: string;
  description: string;
  departmentId?: number | null;
}

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

export interface CreateDepartment {
  name: string;
  nameAr: string;
  description: string;
  location: string;
  budget: number;
  establishedDate: string;
  managerId?: number | null;
}

export interface UpdateDepartment {
  id: number;
  name: string;
  nameAr: string;
  description: string;
  location: string;
  budget: number;
  establishedDate: string;
  managerId?: number | null;
}

@Injectable({ providedIn: 'root' })
export class DepartmentService extends ApiClientBase {
  protected readonly endpoint = 'Department';

  constructor(http: HttpClient) {
    super(http);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.url());
  }

  getDepartment(id: number): Observable<Department> {
    return this.http.get<Department>(this.url(`/${id}`));
  }

  createDepartment(dto: CreateDepartment): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateDepartment(id: number, dto: UpdateDepartment): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteDepartment(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}