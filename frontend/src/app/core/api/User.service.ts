import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface User {
  id: number;
  userName?: string | null;
  normalizedUserName?: string | null;
  email?: string | null;
  normalizedEmail?: string | null;
  emailConfirmed: boolean;
  passwordHash?: string | null;
  securityStamp?: string | null;
  concurrencyStamp?: string | null;
  phoneNumber?: string | null;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd?: string | null;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  firstName: string;
  lastName: string;
  nameAR: string;
  opportunitiesId: number;
}

export interface SelectRole {
  roleName: string;
  selected: boolean;
}

export interface ManageUserRoles {
  userId: string;
  userName: string;
  roles: SelectRole[];
}

@Injectable({ providedIn: 'root' })
export class UserService extends ApiClientBase {
  protected readonly endpoint = 'User';

  constructor(http: HttpClient) {
    super(http);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.url());
  }

  getManageRoles(userId?: string): Observable<ManageUserRoles> {
    return this.http.get<ManageUserRoles>(this.url('/ManageRoles'), {
      params: this.params({ userId }),
    });
  }

  manageRoles(dto: ManageUserRoles): Observable<void> {
    return this.http.post<void>(this.url('/ManageRoles'), dto);
  }
}
