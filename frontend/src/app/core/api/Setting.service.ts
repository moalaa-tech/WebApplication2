import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Setting {
  id: number;
  name: string;
  nameAr: string;
  createdDate: string;
  lastModifiedDate?: string | null;
}

export interface CreateSetting {
  name: string;
  nameAr: string;
}

export interface UpdateSetting {
  id: number;
  name: string;
  nameAr: string;
}

@Injectable({ providedIn: 'root' })
export class SettingService extends ApiClientBase {
  protected readonly endpoint = 'Setting';

  constructor(http: HttpClient) {
    super(http);
  }

  getSettings(): Observable<Setting[]> {
    return this.http.get<Setting[]>(this.url());
  }

  getSetting(id: number): Observable<Setting> {
    return this.http.get<Setting>(this.url(`/${id}`));
  }

  createSetting(dto: CreateSetting): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateSetting(id: number, dto: UpdateSetting): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteSetting(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
