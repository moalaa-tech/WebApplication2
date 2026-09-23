import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ProductDto {
  id?: number | null;
  name?: string | null;
  nameAr?: string | null;
  totalCost?: number | null;
  shippingCost?: number | null;
  freightCost?: number | null;
  salesPrice?: number | null;
  productTypeId?: number | null;
  productTypeName?: string | null;
  imageFile?: string | null;
  description?: string | null;
  currentStock?: number | null;
  minimumStockLevel?: number | null;
  sku?: string | null;
}

export interface CreateProductDto {
  name: string;
  nameAr?: string | null;
  totalCost: number;
  shippingCost: number;
  freightCost: number;
  productTypeId?: number | null;
  imageFileUpload?: File;
  imageFile?: string | null;
  description?: string | null;
}

export interface UpdateProductDto {
  id: number;
  name: string;
  nameAr?: string | null;
  totalCost?: number | null;
  shippingCost?: number | null;
  freightCost?: number | null;
  productTypeId?: number | null;
  existingImageFile?: string | null;
  description?: string | null;
}

@Injectable({ providedIn: 'root' })
export class ProductService extends ApiClientBase {
  protected readonly endpoint = 'Product';

  constructor(http: HttpClient) {
    super(http);
  }

  getProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(this.url());
  }

  getProduct(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(this.url(`/${id}`));
  }

  createProduct(dto: CreateProductDto): Observable<CreateProductDto> {
    const form = new FormData();
    form.append('Name', dto.name);
    if (dto.nameAr) {
      form.append('NameAr', dto.nameAr);
    }
    form.append('TotalCost', String(dto.totalCost));
    form.append('ShippingCost', String(dto.shippingCost));
    form.append('FreightCost', String(dto.freightCost));
    if (dto.productTypeId !== null && dto.productTypeId !== undefined) {
      form.append('ProductTypeId', String(dto.productTypeId));
    }
    if (dto.imageFileUpload) {
      form.append('ImageFileUpload', dto.imageFileUpload);
    }
    if (dto.imageFile) {
      form.append('ImageFile', dto.imageFile);
    }
    if (dto.description) {
      form.append('Description', dto.description);
    }
    return this.http.post<CreateProductDto>(this.url(), form);
  }

  updateProduct(id: number, dto: UpdateProductDto): Observable<UpdateProductDto> {
    return this.http.put<UpdateProductDto>(this.url(`/${id}`), dto);
  }

  getUpload(): Observable<void> {
    return this.http.get<void>(this.url('/Upload'));
  }
}