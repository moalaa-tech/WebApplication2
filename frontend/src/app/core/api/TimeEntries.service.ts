import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface TimeEntry {
  id: number;
  jobPhaseId: number;
  phaseName: string;
  userId: string;
  userName: string;
  userEmail: string;
  entryDate: string;
  hours: number;
  description: string;
  isBillable: boolean;
  rate: number;
  totalCost: number;
  status: string | null;
  isApproved: boolean;
  createdDate: string;
  modifiedDate: string | null;
}

export interface CreateTimeEntry {
  jobPhaseId: number;
  userId: string;
  entryDate: string;
  hours: number;
  description: string;
  isBillable: boolean;
  rate: number;
  status?: number;
  phaseStartDate?: string;
  phaseEndDate?: string | null;
  maxDailyHours?: number;
}

export interface UpdateTimeEntry {
  id: number;
  entryDate: string;
  hours: number;
  description: string;
  isBillable: boolean;
  rate: number;
  status: number;
  createdDate: string;
  createdBy: string;
  modifiedDate?: string | null;
  modifiedBy: string;
  phaseStartDate: string;
  phaseEndDate?: string | null;
  maxDailyHours: number;
}

@Injectable({ providedIn: 'root' })
export class TimeEntriesService extends ApiClientBase {
  protected readonly endpoint = 'TimeEntries';

  constructor(http: HttpClient) {
    super(http);
  }

  getTimeEntries(fromDate?: string, toDate?: string): Observable<TimeEntry[]> {
    return this.http.get<TimeEntry[]>(this.url(), { params: this.params({ fromDate, toDate }) });
  }

  getTimeEntry(id: number): Observable<TimeEntry> {
    return this.http.get<TimeEntry>(this.url(`/${id}`));
  }

  createTimeEntry(model: CreateTimeEntry): Observable<CreateTimeEntry> {
    return this.http.post<CreateTimeEntry>(this.url(), model);
  }

  updateTimeEntry(id: number, model: UpdateTimeEntry): Observable<UpdateTimeEntry> {
    return this.http.put<UpdateTimeEntry>(this.url(`/${id}`), model);
  }

  deleteTimeEntry(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  approveTimeEntry(id: number): Observable<void> {
    return this.http.post<void>(this.url(`/${id}/approve`), null);
  }

  rejectTimeEntry(id: number): Observable<void> {
    return this.http.post<void>(this.url(`/${id}/reject`), null);
  }
}