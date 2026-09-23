import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Notification {
  id: number;
  dateModified?: string | null;
  dateCreated?: string | null;
  isActive: boolean;
  userId: string;
  message: string;
  isRead: boolean;
}

export interface CreateNotification {
  userId: string;
  message: string;
  isRead: boolean;
}

export interface UpdateNotification {
  id: number;
  userId: string;
  message: string;
  isRead: boolean;
  isActive: boolean;
}

@Injectable({ providedIn: 'root' })
export class NotificationsService extends ApiClientBase {
  protected readonly endpoint = 'Notifications';

  constructor(http: HttpClient) {
    super(http);
  }

  getNotifications(): Observable<Notification[]> {
    return this.http.get<Notification[]>(this.url());
  }

  getNotification(id: number): Observable<Notification> {
    return this.http.get<Notification>(this.url(`/${id}`));
  }

  createNotification(dto: CreateNotification): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateNotification(id: number, dto: UpdateNotification): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteNotification(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  markAsRead(id: number): Observable<void> {
    return this.http.post<void>(this.url('/MarkAsRead'), null, {
      params: this.params({ id }),
    });
  }
}
