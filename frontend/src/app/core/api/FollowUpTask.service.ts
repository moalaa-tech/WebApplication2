import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface FollowUpTask {
  id: number;
  subject: string | null;
  dueDate: string;
  contactName: string | null;
  status: number;
  priority: number;
  description: string | null;
  createdById: number;
  isRecurring: boolean;
  recurrenceDays: number | null;
  completed: boolean;
}

export interface CreateFollowUpTask {
  subject?: string | null;
  dueDate: string;
  contactName?: string | null;
  status: number;
  priority: number;
  description?: string | null;
  createdById: number;
  isRecurring: boolean;
  recurrenceDays?: number | null;
}

export interface UpdateFollowUpTask extends CreateFollowUpTask {
  id: number;
  completed: boolean;
}

@Injectable({ providedIn: 'root' })
export class FollowUpTaskService extends ApiClientBase {
  protected readonly endpoint = 'FollowUpTask';

  constructor(http: HttpClient) {
    super(http);
  }

  getFollowUpTasks(): Observable<FollowUpTask[]> {
    return this.http.get<FollowUpTask[]>(this.url());
  }

  getFollowUpTask(id: number): Observable<FollowUpTask> {
    return this.http.get<FollowUpTask>(this.url(`/${id}`));
  }

  createFollowUpTask(dto: CreateFollowUpTask): Observable<CreateFollowUpTask> {
    return this.http.post<CreateFollowUpTask>(this.url(), dto);
  }

  updateFollowUpTask(id: number, dto: UpdateFollowUpTask): Observable<UpdateFollowUpTask> {
    return this.http.put<UpdateFollowUpTask>(this.url(`/${id}`), dto);
  }

  deleteFollowUpTask(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  markComplete(id: number): Observable<FollowUpTask> {
    return this.http.post<FollowUpTask>(this.url('/MarkComplete'), null, {
      params: this.params({ id }),
    });
  }
}