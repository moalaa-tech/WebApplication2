import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Automation {
  id: number;
  name: string;
  description: string | null;
  trigger: number;
  lastRunDate: string | null;
  customEventName: string | null;
  isActive: boolean;
}

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

export interface AutomationContact {
  contactId: number;
  contactName: string;
  contactEmail: string;
  dateAdded: string;
  currentStep: number;
  currentStepName: string;
  nextStepDate: string | null;
}

export interface AutomationRunRecord {
  runDate: string;
  contactsProcessed: number;
  emailsSent: number;
  statusChanges: number;
  campaignAdditions: number;
}

export interface AutomationRunHistory {
  recentRuns: AutomationRunRecord[] | null;
}

export interface ContactSelection {
  id: number;
  name: string;
  email: string;
  company: string;
  isSelected: boolean;
}

export interface RunAutomation {
  automationId: number;
  automationName: string;
  selectedContactIds: number[];
  contacts: ContactSelection[];
}

export interface AutomationViewModel {
  automation: Automation;
  availableTriggers?: number[];
  steps: AutomationStep[];
  activeContacts?: AutomationContact[];
  runHistory?: AutomationRunHistory | null;
  triggerOptions?: unknown;
  actionOptions?: unknown;
  emailTemplateOptions?: unknown;
  campaignOptions?: unknown;
  statusOptions?: unknown;
  emailTemplates?: unknown;
  availableActions?: number[];
}

@Injectable({ providedIn: 'root' })
export class AutomationsService extends ApiClientBase {
  protected readonly endpoint = 'Automations';

  constructor(http: HttpClient) {
    super(http);
  }

  getAutomations(): Observable<Automation[]> {
    return this.http.get<Automation[]>(this.url());
  }

  createAutomation(model: AutomationViewModel): Observable<AutomationViewModel> {
    return this.http.post<AutomationViewModel>(this.url(), model);
  }

  getAutomationRun(id: number): Observable<RunAutomation> {
    return this.http.get<RunAutomation>(this.url('/Run'), { params: this.params({ id }) });
  }

  runAutomation(model: RunAutomation): Observable<void> {
    return this.http.post<void>(this.url('/Run'), model);
  }

  toggleAutomationStatus(id: number): Observable<void> {
    return this.http.post<void>(this.url('/ToggleStatus'), null, { params: this.params({ id }) });
  }
}