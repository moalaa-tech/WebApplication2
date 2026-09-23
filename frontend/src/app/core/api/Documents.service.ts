import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Document {
  id: number;
  title: string;
  description: string;
  documentType: string;
  fileName: string;
  fileSize: number;
  fileExtension: string;
  categoryName: string;
  folderName: string;
  version: number;
  uploadedBy: string;
  uploadDate: string;
  modifiedDate?: string | null;
  expiryDate?: string | null;
  isPublic: boolean;
  status: string;
  downloadCount: number;
  fileSizeFormatted: string;
  tags: string;
}

export interface CreateDocument {
  title: string;
  description: string;
  documentType: string;
  categoryId?: number | null;
  folderId?: number | null;
  expiryDate?: string | null;
  isPublic: boolean;
  tags: string;
  file: File;
}

export interface UpdateDocument {
  id: number;
  title: string;
  description: string;
  documentType: string;
  categoryId?: number | null;
  folderId?: number | null;
  expiryDate?: string | null;
  isPublic: boolean;
  status: string;
  tags: string;
  file: File;
  currentFileName: string;
}

export interface ShareDocumentRequest {
  documentId: number;
  sharedWithUserId: number;
  permissionLevel: string;
  expiryDate?: string | null;
  canDownload: boolean;
}

@Injectable({ providedIn: 'root' })
export class DocumentsService extends ApiClientBase {
  protected readonly endpoint = 'Documents';

  constructor(http: HttpClient) {
    super(http);
  }

  getDocuments(searchTerm?: string): Observable<Document[]> {
    return this.http.get<Document[]>(this.url(), { params: this.params({ searchTerm }) });
  }

  getDocument(id: number): Observable<Document> {
    return this.http.get<Document>(this.url(`/${id}`));
  }

  createDocument(viewModel: CreateDocument): Observable<void> {
    return this.http.post<void>(this.url(), viewModel);
  }

  updateDocument(id: number, viewModel: UpdateDocument): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), viewModel);
  }

  deleteDocument(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  downloadDocument(id: number): Observable<Blob> {
    return this.http.get(this.url(`/Download/${id}`), { responseType: 'blob' });
  }

  shareDocument(dto: ShareDocumentRequest): Observable<void> {
    return this.http.post<void>(this.url('/Share'), dto);
  }
}