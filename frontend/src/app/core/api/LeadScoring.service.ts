import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface LeadScoreRuleDto {
  id: number;
  ruleName: string;
  condition: string;
  points: number;
  executionOrder: number;
  isActive: boolean;
}

export interface LeadScoreDto {
  id: number;
  name: string;
  description: string;
  score: number;
  isActive: boolean;
  criteria: string;
  scoreType: string;
  createdDate: string;
  lastModifiedDate?: string | null;
  modifiedBy: string;
  rules: LeadScoreRuleDto[];
}

export interface LeadScoreCreateDto {
  name: string;
  description: string;
  score: number;
  criteria: string;
  scoreType: string;
  rules: LeadScoreRuleDto[];
}

export interface LeadScoreUpdateDto {
  id: number;
  name: string;
  description: string;
  score: number;
  isActive: boolean;
  criteria: string;
  scoreType: string;
  rules: LeadScoreRuleDto[];
}

export interface LeadScoreIndexViewModel {
  leadScores: LeadScoreDto[];
  scoreTypeCounts?: Record<string, number> | null;
}

export interface LeadScoreDetailsViewModel {
  leadScore: LeadScoreDto;
}

export interface LeadScoreCreateViewModel {
  leadScoreCreateDto: LeadScoreCreateDto;
  availableScoreTypes?: string[];
}

export interface LeadScoreEditViewModel {
  leadScoreUpdateDto: LeadScoreUpdateDto;
  availableScoreTypes?: string[];
}

@Injectable({ providedIn: 'root' })
export class LeadScoringService extends ApiClientBase {
  protected readonly endpoint = 'LeadScoring';

  constructor(http: HttpClient) {
    super(http);
  }

  getLeadScores(): Observable<LeadScoreIndexViewModel> {
    return this.http.get<LeadScoreIndexViewModel>(this.url());
  }

  getLeadScore(id: number): Observable<LeadScoreDetailsViewModel> {
    return this.http.get<LeadScoreDetailsViewModel>(this.url(`/${id}`));
  }

  createLeadScore(viewModel: LeadScoreCreateViewModel): Observable<LeadScoreCreateViewModel> {
    return this.http.post<LeadScoreCreateViewModel>(this.url(), viewModel);
  }

  updateLeadScore(id: number, viewModel: LeadScoreEditViewModel): Observable<LeadScoreEditViewModel> {
    return this.http.put<LeadScoreEditViewModel>(this.url(`/${id}`), viewModel);
  }

  deleteLeadScore(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getLeadScoresByType(scoreType?: string): Observable<LeadScoreIndexViewModel> {
    return this.http.get<LeadScoreIndexViewModel>(this.url('/ByType'), { params: this.params({ scoreType }) });
  }
}