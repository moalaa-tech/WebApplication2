import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Role {
  id: number;
  name?: string | null;
  normalizedName?: string | null;
  concurrencyStamp?: string | null;
  nameAR: string;
}

@Injectable({ providedIn: 'root' })
export class RoleService extends ApiClientBase {
  protected readonly endpoint = 'Role';

  constructor(http: HttpClient) {
    super(http);
  }

  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(this.url());
  }

  createRole(dto: Role): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateRole(id: number, dto: Role): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }
}
