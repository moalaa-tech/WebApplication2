import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

@Injectable({ providedIn: 'root' })
export class InstagramWebhookService extends ApiClientBase {
  protected readonly endpoint = 'InstagramWebhook';

  constructor(http: HttpClient) {
    super(http);
  }

  login(): Observable<unknown> {
    return this.http.get<unknown>(this.url('/login'));
  }

  callback(code: string): Observable<string> {
    return this.http.get<string>(this.url('/callback'), { params: this.params({ code }) });
  }

  verifyWebhook(mode: string, challenge: string, verifyToken: string): Observable<string> {
    return this.http.get<string>(this.url(), {
      params: this.params({ 'hub.mode': mode, 'hub.challenge': challenge, 'hub.verify_token': verifyToken })
    });
  }

  handleWebhook(payload: Record<string, unknown>): Observable<void> {
    return this.http.post<void>(this.url(), payload);
  }
}