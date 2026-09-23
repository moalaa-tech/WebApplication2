import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface SelectListItem {
  value: string;
  text: string;
  selected?: boolean;
}

export interface StockInDto {
  productId: number;
  quantity: number;
  cost?: number | null;
  notes?: string | null;
}

export interface StockOutDto {
  productId: number;
  quantity: number;
  cost?: number | null;
  notes?: string | null;
}

export interface StockAdjustmentDto {
  productId: number;
  newQuantity: number;
  notes?: string | null;
}

@Injectable({ providedIn: 'root' })
export class StockService extends ApiClientBase {
  protected readonly endpoint = 'Stock';

  constructor(http: HttpClient) {
    super(http);
  }

  getStockIn(): Observable<SelectListItem[]> {
    return this.http.get<SelectListItem[]>(this.url('/In'));
  }

  stockIn(dto: StockInDto): Observable<StockInDto> {
    return this.http.post<StockInDto>(this.url('/In'), dto);
  }

  getStockOut(): Observable<SelectListItem[]> {
    return this.http.get<SelectListItem[]>(this.url('/Out'));
  }

  stockOut(dto: StockOutDto): Observable<StockOutDto> {
    return this.http.post<StockOutDto>(this.url('/Out'), dto);
  }

  getStockAdjust(): Observable<SelectListItem[]> {
    return this.http.get<SelectListItem[]>(this.url('/Adjust'));
  }

  adjustStock(dto: StockAdjustmentDto): Observable<StockAdjustmentDto> {
    return this.http.post<StockAdjustmentDto>(this.url('/Adjust'), dto);
  }
}