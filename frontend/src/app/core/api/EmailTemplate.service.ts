import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface EmailTemplate {
  id: number;
  name: string;
  subject: string;
  content: string;
}

@Injectable({ providedIn: 'root' })
export class EmailTemplateService extends ApiClientBase {
  protected readonly endpoint = 'EmailTemplate';

  constructor(http: HttpClient) {
    super(http);
  }

  getEmailTemplates(): Observable<EmailTemplate[]> {
    return this.http.get<EmailTemplate[]>(this.url());
  }

  getEmailTemplate(id: number): Observable<EmailTemplate> {
    return this.http.get<EmailTemplate>(this.url(`/${id}`));
  }

  createEmailTemplate(template: EmailTemplate): Observable<EmailTemplate> {
    return this.http.post<EmailTemplate>(this.url(), template);
  }

  updateEmailTemplate(id: number, template: EmailTemplate): Observable<EmailTemplate> {
    return this.http.put<EmailTemplate>(this.url(`/${id}`), template);
  }

  deleteEmailTemplate(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}