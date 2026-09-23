import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export type CampaignStatus = 'Planned' | 'Active' | 'Paused' | 'Completed' | 'Cancelled';
export type ContactStatus = string;
export type InteractionStatus = string;

export interface ContactViewModel {
  id: number;
  name: string;
  nameAr: string;
  surname: string;
  fullName: string;
  phone: string;
  email: string;
  position: string;
  companyId: number;
  companyName: string;
  lastContactDate?: string | null;
  status: ContactStatus;
}

export interface InteractionViewModel {
  id: number;
  date: string;
  type: string;
  notes: string;
  status: InteractionStatus;
  contactId: number;
  contactName: string;
  campaignId: number;
}

export interface CampaignDto {
  id: number;
  name: string;
  description: string;
  startDate: string;
  endDate?: string | null;
  budget: number;
  status: CampaignStatus;
  emailTemplateId?: number | null;
  emailTemplateName: string;
  contactCount: number;
  interactionCount: number;
}

export interface CampaignCreateViewModel {
  name: string;
  description: string;
  startDate: string;
  endDate?: string | null;
  budget: number;
  emailTemplateId?: number | null;
}

export interface CampaignEditViewModel {
  id: number;
  name: string;
  description: string;
  startDate: string;
  endDate?: string | null;
  budget: number;
  emailTemplateId?: number | null;
}

export interface CampaignDetailsViewModel {
  id: number;
  name: string;
  description: string;
  startDate: string;
  endDate?: string | null;
  budget: number;
  status: CampaignStatus;
  emailTemplateName: string;
  contactCount: number;
  interactionCount: number;
  contacts: ContactViewModel[];
  interactions: InteractionViewModel[];
}

@Injectable({ providedIn: 'root' })
export class CampaignsService extends ApiClientBase {
  protected readonly endpoint = 'Campaigns';

  constructor(http: HttpClient) {
    super(http);
  }

  getCampaigns(): Observable<CampaignDto[]> {
    return this.http.get<CampaignDto[]>(this.url());
  }

  getCampaign(id: number): Observable<CampaignDetailsViewModel> {
    return this.http.get<CampaignDetailsViewModel>(this.url(`/${id}`));
  }

  createCampaign(viewModel: CampaignCreateViewModel): Observable<CampaignCreateViewModel> {
    return this.http.post<CampaignCreateViewModel>(this.url(), viewModel);
  }

  updateCampaign(id: number, viewModel: CampaignEditViewModel): Observable<CampaignEditViewModel> {
    return this.http.put<CampaignEditViewModel>(this.url(`/${id}`), viewModel);
  }

  changeStatus(id: number, status: CampaignStatus): Observable<void> {
    return this.http.post<void>(this.url(`/ChangeStatus/${id}`), null, { params: this.params({ status }) });
  }
}