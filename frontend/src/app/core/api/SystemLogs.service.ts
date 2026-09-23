import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface LogFilter {
  page?: number;
  pageSize?: number;
  level?: string;
  search?: string;
  fromDate?: string;
  toDate?: string;
}

export interface Log {
  id: number;
  timestamp: string;
  level: string;
  message: string;
  exception: string;
  logger: string;
  url: string;
  httpMethod: string;
  userName: string;
  clientIP: string;
  statusCode?: number | null;
  requestBody: string;
  responseBody: string;
  duration?: number | null;
}

export interface LogSummary {
  totalCount: number;
  informationCount: number;
  warningCount: number;
  errorCount: number;
  criticalCount: number;
}

export interface LogIndex {
  logs: Log[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  levelFilter: string;
  searchFilter: string;
  logSummary: LogSummary;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

@Injectable({ providedIn: 'root' })
export class SystemLogsService extends ApiClientBase {
  protected readonly endpoint = 'SystemLogs';

  constructor(http: HttpClient) {
    super(http);
  }

  getSystemLogs(filter?: LogFilter): Observable<LogIndex> {
    return this.http.get<LogIndex>(this.url(), {
      params: this.params({ ...filter }),
    });
  }

  getSystemLog(id: number): Observable<Log> {
    return this.http.get<Log>(this.url(`/${id}`));
  }

  clearLogs(daysToKeep?: number): Observable<void> {
    return this.http.post<void>(this.url('/ClearLogs'), null, {
      params: this.params({ daysToKeep }),
    });
  }
}
