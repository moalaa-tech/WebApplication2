import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface DocumentFolder {
  id: number;
  name: string;
  description: string;
  parentFolderId?: number | null;
  parentFolderName: string;
  path: string;
  isSystemFolder: boolean;
  isActive: boolean;
  createdAt: string;
  updatedAt?: string | null;
  documentCount: number;
  subFolderCount: number;
  subFolders: DocumentFolder[];
}

export interface FolderTree {
  id: number;
  name: string;
  path: string;
  parentFolderId?: number | null;
  documentCount: number;
  children: FolderTree[];
}

export interface CreateDocumentFolder {
  name: string;
  description: string;
  parentFolderId?: number | null;
  isSystemFolder: boolean;
}

export interface UpdateDocumentFolder {
  id: number;
  name: string;
  description: string;
  parentFolderId?: number | null;
  isSystemFolder: boolean;
  isActive: boolean;
}

@Injectable({ providedIn: 'root' })
export class DocumentFoldersService extends ApiClientBase {
  protected readonly endpoint = 'DocumentFolders';

  constructor(http: HttpClient) {
    super(http);
  }

  getDocumentFolders(): Observable<DocumentFolder[]> {
    return this.http.get<DocumentFolder[]>(this.url());
  }

  getDocumentFolder(id: number): Observable<DocumentFolder> {
    return this.http.get<DocumentFolder>(this.url(`/${id}`));
  }

  createDocumentFolder(dto: CreateDocumentFolder): Observable<DocumentFolder> {
    return this.http.post<DocumentFolder>(this.url(), dto);
  }

  updateDocumentFolder(id: number, dto: UpdateDocumentFolder): Observable<DocumentFolder> {
    return this.http.put<DocumentFolder>(this.url(`/${id}`), dto);
  }

  deleteDocumentFolder(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getFolderTree(): Observable<FolderTree[]> {
    return this.http.get<FolderTree[]>(this.url('/TreeView'));
  }
}
