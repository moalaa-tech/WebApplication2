import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface WarehouseDto {
  id: number;
  name: string;
  location: string;
  manager: string;
}

@Injectable({ providedIn: 'root' })
export class WarehouseService extends ApiClientBase {
  protected readonly endpoint = 'Warehouse';

  constructor(http: HttpClient) {
    super(http);
  }

  getWarehouses(): Observable<WarehouseDto[]> {
    return this.http.get<WarehouseDto[]>(this.url());
  }

  createWarehouse(dto: WarehouseDto): Observable<WarehouseDto> {
    return this.http.post<WarehouseDto>(this.url(), dto);
  }

  updateWarehouse(dto: WarehouseDto): Observable<WarehouseDto> {
    return this.http.put<WarehouseDto>(this.url('/Edit'), dto);
  }

  deleteWarehouse(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}