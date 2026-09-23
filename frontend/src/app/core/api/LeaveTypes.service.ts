import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface LeaveType {
  id: number;
  name: string;
  nameAr: string;
}

export interface CreateLeaveType {
  name: string;
  nameAr: string;
}

export interface UpdateLeaveType {
  id: number;
  name: string;
  nameAr: string;
}

@Injectable({ providedIn: 'root' })
export class LeaveTypesService extends ApiClientBase {
  protected readonly endpoint = 'LeaveTypes';

  constructor(http: HttpClient) {
    super(http);
  }

  getLeaveTypes(): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(this.url());
  }

  getLeaveType(id: number): Observable<LeaveType> {
    return this.http.get<LeaveType>(this.url(`/${id}`));
  }

  createLeaveType(dto: CreateLeaveType): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateLeaveType(id: number, dto: UpdateLeaveType): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteLeaveType(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}