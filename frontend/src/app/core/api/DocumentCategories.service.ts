import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface DocumentCategory {
  id: number;
  name: string;
  description: string;
  parentName: string;
  isActive: boolean;
  order: number;
  documentCount: number;
  slug: string;
  parentId?: number | null;
  createdAt: string;
}

export interface CreateDocumentCategory {
  name: string;
  nameAr: string;
  description: string;
  parentCategoryId?: number | null;
  isActive: boolean;
  order: number;
}

export interface UpdateDocumentCategory {
  id: number;
  name: string;
  description: string;
  parentCategoryId?: number | null;
  isActive: boolean;
  order: number;
}

@Injectable({ providedIn: 'root' })
export class DocumentCategoriesService extends ApiClientBase {
  protected readonly endpoint = 'DocumentCategories';

  constructor(http: HttpClient) {
    super(http);
  }

  getDocumentCategories(): Observable<DocumentCategory[]> {
    return this.http.get<DocumentCategory[]>(this.url());
  }

  getDocumentCategory(id: number): Observable<DocumentCategory> {
    return this.http.get<DocumentCategory>(this.url(`/${id}`));
  }

  createDocumentCategory(dto: CreateDocumentCategory): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  updateDocumentCategory(id: number, dto: UpdateDocumentCategory): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteDocumentCategory(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}
