import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface JournalLine {
  accountId: number;
  accountName: string;
  accountCode: string;
  debit: number;
  credit: number;
  description: string;
}

export interface CreateJournalEntry {
  lines: JournalLine[];
}

export interface JournalEntry {
  id: number;
  entryDate: string;
  reference: string;
  description: string;
  status: string;
  totalAmount: string;
  debit: string;
  credit: string;
}

export interface JournalIndex {
  fromDate?: string | null;
  toDate?: string | null;
  postedOnly?: boolean | null;
  entries: JournalEntry[];
}

@Injectable({ providedIn: 'root' })
export class JournalService extends ApiClientBase {
  protected readonly endpoint = 'Journal';

  constructor(http: HttpClient) {
    super(http);
  }

  getJournals(fromDate?: string | null, toDate?: string | null, postedOnly?: boolean | null): Observable<JournalIndex> {
    return this.http.get<JournalIndex>(this.url(), {
      params: this.params({ fromDate, toDate, postedOnly }),
    });
  }

  createJournalEntry(dto: CreateJournalEntry): Observable<CreateJournalEntry> {
    return this.http.post<CreateJournalEntry>(this.url(), dto);
  }

  postJournalEntry(id: number): Observable<void> {
    return this.http.post<void>(this.url(`/Post/${id}`), null);
  }
}
