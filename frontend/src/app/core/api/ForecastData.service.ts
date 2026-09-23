import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface DropdownOption {
  value?: number | null;
  text: string;
}

export interface ForecastData {
  id: number;
  dataType: string;
  dataDate: string;
  value: number;
  valueDecimal: number;
  notes: string;
  demandPlanId: number;
  supplyChainEventId?: number | null;
  itemId?: number | null;
  shippingId?: number | null;
  freightId?: number | null;
  historicalDataId?: number | null;
  demandPlanName?: string;
  supplyChainEventName?: string;
  itemName?: string;
  shippingReference?: string;
  freightReference?: string;
  historicalDataPeriod?: string;
  createdDate?: string;
  modifiedDate?: string | null;
  dataTypes?: DropdownOption[];
  demandPlans?: DropdownOption[];
  supplyChainEvents?: DropdownOption[];
  items?: DropdownOption[];
  shippingOptions?: DropdownOption[];
  freightOptions?: DropdownOption[];
  historicalDataOptions?: DropdownOption[];
}

export interface ForecastDataFilter {
  dataType?: string;
  startDate?: string | null;
  endDate?: string | null;
  demandPlanId?: number | null;
  results?: ForecastData[];
  dataTypes?: DropdownOption[];
  demandPlans?: DropdownOption[];
}

export interface ForecastSummary {
  total: number;
  average: number;
  max: number;
  min: number;
}

@Injectable({ providedIn: 'root' })
export class ForecastDataService extends ApiClientBase {
  protected readonly endpoint = 'ForecastData';

  constructor(http: HttpClient) {
    super(http);
  }

  getForecastData(): Observable<ForecastData[]>;
  getForecastData(id: number): Observable<ForecastData>;
  getForecastData(id?: number): Observable<ForecastData[] | ForecastData> {
    if (id === undefined) {
      return this.http.get<ForecastData[]>(this.url());
    }
    return this.http.get<ForecastData>(this.url(`/${id}`));
  }

  createForecastData(model: ForecastData): Observable<ForecastData> {
    return this.http.post<ForecastData>(this.url(), model);
  }

  updateForecastData(id: number, model: ForecastData): Observable<ForecastData> {
    return this.http.put<ForecastData>(this.url(`/${id}`), model);
  }

  deleteForecastData(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getFilter(): Observable<ForecastDataFilter> {
    return this.http.get<ForecastDataFilter>(this.url('/Filter'));
  }

  filter(model: ForecastDataFilter): Observable<ForecastDataFilter> {
    return this.http.post<ForecastDataFilter>(this.url('/Filter'), model);
  }

  getSummary(): Observable<ForecastSummary> {
    return this.http.get<ForecastSummary>(this.url('/Summary'));
  }
}