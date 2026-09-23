import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface RegisterViewModel {
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
}

export interface LoginViewModel {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface AuthResult {
  succeeded: boolean;
  message?: string;
  errors?: string[];
}

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends ApiClientBase {
  protected readonly endpoint = 'Authentication';

  constructor(http: HttpClient) {
    super(http);
  }

  register(dto: RegisterViewModel): Observable<AuthResult> {
    return this.http.post<AuthResult>(this.url('/register'), dto);
  }

  login(dto: LoginViewModel): Observable<AuthResult> {
    return this.http.post<AuthResult>(this.url('/login'), dto);
  }

  logout(): Observable<AuthResult> {
    return this.http.post<AuthResult>(this.url('/logout'), null);
  }
}
