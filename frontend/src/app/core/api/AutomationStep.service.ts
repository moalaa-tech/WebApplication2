import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface AutomationStep {
  id: number;
  automationId: number;
  order: number;
  action: number;
  emailTemplateId?: number | null;
  emailTemplateName: string | null;
  waitDays?: number | null;
  statusValue: string | null;
  campaignId?: number | null;
  campaignName: string | null;
}

@Injectable({ providedIn: 'root' })
export class AutomationStepService extends ApiClientBase {
  protected readonly endpoint = 'AutomationStep';

  constructor(http: HttpClient) {
    super(http);
  }

  getAutomationSteps(): Observable<AutomationStep[]> {
    return this.http.get<AutomationStep[]>(this.url());
  }

  getAutomationStep(id: number): Observable<AutomationStep> {
    return this.http.get<AutomationStep>(this.url(`/${id}`));
  }

  createAutomationStep(vm: AutomationStep): Observable<AutomationStep> {
    return this.http.post<AutomationStep>(this.url(), vm);
  }

  updateAutomationStep(id: number, vm: AutomationStep): Observable<AutomationStep> {
    return this.http.put<AutomationStep>(this.url(`/${id}`), vm);
  }

  deleteAutomationStep(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}