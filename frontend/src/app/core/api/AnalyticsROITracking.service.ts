import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface CampaignAnalyticsDto {
  id: number;
  campaignId: number;
  campaignName: string;
  startDate: string;
  endDate: string;
  totalBudget: number;
  totalSpent: number;
  impressions: number;
  clicks: number;
  conversions: number;
  conversionRate: number;
  costPerClick: number;
  costPerConversion: number;
  revenueGenerated: number;
  roiPercentage: number;
  recordedDate: string;
  lastUpdated?: string | null;
  notes: string;
  channel: string;
}

export interface CampaignAnalyticsCreateDto {
  campaignId: string;
  campaignName: string;
  startDate: string;
  endDate: string;
  totalBudget: number;
  totalSpent: number;
  impressions: number;
  clicks: number;
  conversions: number;
  conversionRate: number;
  costPerClick: number;
  costPerConversion: number;
  revenueGenerated: number;
  roiPercentage: number;
  notes: string;
  channel: string;
}

export interface CampaignAnalyticsUpdateDto {
  id: number;
  campaignId: string;
  campaignName: string;
  startDate: string;
  endDate: string;
  totalBudget: number;
  totalSpent: number;
  impressions: number;
  clicks: number;
  conversions: number;
  conversionRate: number;
  costPerClick: number;
  costPerConversion: number;
  revenueGenerated: number;
  roiPercentage: number;
  notes: string;
  channel: string;
}

export interface CampaignAnalyticsCreateViewModel {
  campaignAnalyticsCreateDto: CampaignAnalyticsCreateDto;
}

export interface CampaignAnalyticsEditViewModel {
  campaignAnalyticsUpdateDto: CampaignAnalyticsUpdateDto;
}

export interface CampaignAnalyticsIndexViewModel {
  analytics: CampaignAnalyticsDto[];
  channelPerformance: Record<string, number>;
  totalROI: number;
}

export interface CampaignAnalyticsDetailsViewModel {
  campaignAnalytics: CampaignAnalyticsDto;
}

export interface ROICalculatorViewModel {
  investment: number;
  revenue: number;
  calculatedROI: number;
  channelROIs?: Record<string, number> | null;
}

@Injectable({ providedIn: 'root' })
export class AnalyticsROITrackingService extends ApiClientBase {
  protected readonly endpoint = 'AnalyticsROITracking';

  constructor(http: HttpClient) {
    super(http);
  }

  getAnalytics(): Observable<CampaignAnalyticsIndexViewModel>;
  getAnalytics(id: number): Observable<CampaignAnalyticsDetailsViewModel>;
  getAnalytics(
    id?: number,
  ): Observable<CampaignAnalyticsIndexViewModel | CampaignAnalyticsDetailsViewModel> {
    return id === undefined
      ? this.http.get<CampaignAnalyticsIndexViewModel>(this.url())
      : this.http.get<CampaignAnalyticsDetailsViewModel>(this.url(`/${id}`));
  }

  createAnalytics(viewModel: CampaignAnalyticsCreateViewModel): Observable<CampaignAnalyticsCreateViewModel> {
    return this.http.post<CampaignAnalyticsCreateViewModel>(this.url(), viewModel);
  }

  updateAnalytics(id: number, viewModel: CampaignAnalyticsEditViewModel): Observable<CampaignAnalyticsEditViewModel> {
    return this.http.put<CampaignAnalyticsEditViewModel>(this.url(`/${id}`), viewModel);
  }

  deleteAnalytics(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getROICalculator(): Observable<ROICalculatorViewModel> {
    return this.http.get<ROICalculatorViewModel>(this.url('/ROICalculator'));
  }

  calculateROI(viewModel: ROICalculatorViewModel): Observable<ROICalculatorViewModel> {
    return this.http.post<ROICalculatorViewModel>(this.url('/ROICalculator'), viewModel);
  }
}