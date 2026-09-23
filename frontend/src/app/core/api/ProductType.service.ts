import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ProductTypeDto {
  id: number;
  name?: string | null;
  nameAR?: string | null;
  description?: string | null;
}

export interface CreateProductTypeDto {
  name: string;
  nameAR: string;
  description?: string | null;
}

export interface UpdateProductTypeDto {
  id: number;
  name: string;
  description?: string | null;
}

@Injectable({ providedIn: 'root' })
export class ProductTypeService extends ApiClientBase {
  protected readonly endpoint = 'ProductType';

  constructor(http: HttpClient) {
    super(http);
  }

  getProductTypes(): Observable<ProductTypeDto[]> {
    return this.http.get<ProductTypeDto[]>(this.url());
  }

  getProductType(id: number): Observable<ProductTypeDto> {
    return this.http.get<ProductTypeDto>(this.url(`/${id}`));
  }

  createProductType(dto: CreateProductTypeDto): Observable<CreateProductTypeDto> {
    return this.http.post<CreateProductTypeDto>(this.url(), dto);
  }

  updateProductType(dto: UpdateProductTypeDto): Observable<UpdateProductTypeDto> {
    return this.http.put<UpdateProductTypeDto>(this.url('/Edit'), dto);
  }

  deleteProductType(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}