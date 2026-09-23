import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface FixedAsset {
  id: number;
  assetNumber: string;
  name: string;
  description: string;
  acquisitionDate: string;
  acquisitionCost: number;
  salvageValue: number;
  usefulLife: number;
  depreciationMethod: number;
  depreciationMethodName: string;
  parentAssetId?: number | null;
  parentAssetName: string;
  currentBookValue: number;
  totalDepreciation: number;
  remainingLife: number;
  monthlyDepreciation: number;
  isActive: boolean;
  createdAt: string;
  updatedAt?: string | null;
  childAssetsCount: number;
  depreciationSchedulesCount: number;
}

export interface CreateFixedAsset {
  assetNumber: string;
  name: string;
  description: string;
  acquisitionDate: string;
  acquisitionCost: number;
  salvageValue: number;
  usefulLife: number;
  depreciationMethod: number;
  parentAssetId?: number | null;
}

export interface UpdateFixedAsset extends CreateFixedAsset {
  id: number;
  isActive: boolean;
}

export interface DepreciationSchedule {
  id: number;
  fixedAssetId: number;
  assetNumber: string;
  assetName: string;
  scheduleDate: string;
  depreciationAmount: number;
  accumulatedDepreciation: number;
  bookValue: number;
  isPosted: boolean;
  notes: string;
  createdAt: string;
}

export interface DepreciationScheduleResponse {
  assetName: string | null;
  assetId: number;
  schedule: DepreciationSchedule[];
}

export interface CalculateDepreciationResponse {
  success: boolean;
  depreciation?: number;
  message?: string;
}

@Injectable({ providedIn: 'root' })
export class FixedAssetsService extends ApiClientBase {
  protected readonly endpoint = 'FixedAssets';

  constructor(http: HttpClient) {
    super(http);
  }

  getFixedAssets(): Observable<FixedAsset[]> {
    return this.http.get<FixedAsset[]>(this.url());
  }

  getFixedAsset(id: number): Observable<FixedAsset> {
    return this.http.get<FixedAsset>(this.url(`/${id}`));
  }

  createFixedAsset(dto: CreateFixedAsset): Observable<FixedAsset> {
    return this.http.post<FixedAsset>(this.url(), dto);
  }

  updateFixedAsset(id: number, dto: UpdateFixedAsset): Observable<FixedAsset> {
    return this.http.put<FixedAsset>(this.url(`/${id}`), dto);
  }

  deleteFixedAsset(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getDepreciationSchedule(id: number): Observable<DepreciationScheduleResponse> {
    return this.http.get<DepreciationScheduleResponse>(this.url(`/DepreciationSchedule/${id}`));
  }

  calculateDepreciation(assetId: number, asOfDate: string): Observable<CalculateDepreciationResponse> {
    return this.http.post<CalculateDepreciationResponse>(this.url('/CalculateDepreciation'), null, {
      params: this.params({ assetId, asOfDate }),
    });
  }
}
