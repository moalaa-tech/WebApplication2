import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface DealFile {
  id: number;
  fileName: string;
  filePath: string;
}

export interface Deal {
  id: number;
  opportunityId: number;
  opportunityName: string | null;
  finalValue: number;
  description: string | null;
  dealOwner: string | null;
  name: string | null;
  accountName: string | null;
  type: boolean;
  amount: number;
  closingDate: string;
  contactId: number;
  contactName: string | null;
  files: DealFile[];
}

export interface CreateDeal {
  opportunityId: number;
  finalValue: number;
  description?: string | null;
  dealOwner: string;
  name: string;
  accountName: string;
  type: boolean;
  amount: number;
  closingDate: string;
  contactId: number;
  files?: unknown[];
}

export interface UpdateDeal {
  id: number;
  opportunityId: number;
  finalValue: number;
  description?: string | null;
  dealOwner: string;
  name: string;
  accountName: string;
  type: boolean;
  amount: number;
  closingDate: string;
  contactId: number;
  newFiles?: unknown[];
  filesToDelete?: number[];
}

export interface UploadDealFileResult {
  fileName: string;
}

@Injectable({ providedIn: 'root' })
export class DealsService extends ApiClientBase {
  protected readonly endpoint = 'Deals';

  constructor(http: HttpClient) {
    super(http);
  }

  getDeals(): Observable<Deal[]> {
    return this.http.get<Deal[]>(this.url());
  }

  getDeal(id: number): Observable<Deal> {
    return this.http.get<Deal>(this.url(`/${id}`));
  }

  createDeal(dto: CreateDeal): Observable<Deal> {
    return this.http.post<Deal>(this.url(), dto);
  }

  updateDeal(id: number, dto: UpdateDeal): Observable<UpdateDeal> {
    return this.http.put<UpdateDeal>(this.url(`/${id}`), dto);
  }

  deleteDeal(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  uploadDealFile(file: File): Observable<UploadDealFileResult> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<UploadDealFileResult>(this.url('/UploadDealFile'), formData);
  }
}