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

export interface LeaveType {
  id: number;
  name: string;
  nameAr: string;
}

export interface LeaveRequest {
  id: number;
  employeeId: number;
  employee: Employee;
  leaveTypeId: number;
  leaveType: LeaveType;
  startDate: string;
  endDate: string;
  numberOfDays: number;
  reason?: string | null;
  status?: string | null;
  requestedDate: string;
  approvedBy?: string | null;
  approvedDate?: string | null;
}

export interface CreateLeaveRequest {
  employeeId: number;
  leaveTypeId: number;
  startDate: string;
  endDate: string;
  reason?: string | null;
}

export interface UpdateLeaveRequest {
  id: number;
  status?: string | null;
  approvedBy?: string | null;
  approvedDate?: string | null;
}

@Injectable({ providedIn: 'root' })
export class LeaveRequestService extends ApiClientBase {
  protected readonly endpoint = 'LeaveRequest';

  constructor(http: HttpClient) {
    super(http);
  }

  getLeaveRequests(): Observable<LeaveRequest[]> {
    return this.http.get<LeaveRequest[]>(this.url());
  }

  createLeaveRequest(dto: CreateLeaveRequest): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  getLeaveRequest(id: number): Observable<LeaveRequest> {
    return this.http.get<LeaveRequest>(this.url(`/${id}`));
  }

  updateLeaveRequest(id: number, dto: UpdateLeaveRequest): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteLeaveRequest(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}