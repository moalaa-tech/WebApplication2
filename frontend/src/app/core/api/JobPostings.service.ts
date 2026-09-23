import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface JobPosting {
  id: number;
  title: string;
  description: string;
  requirements: string;
  postingDate: string;
  closingDate: string;
  isActive: boolean;
  departmentId: number;
  departmentName: string;
  applicationCount: number;
}

export interface CreateJobPosting {
  title: string;
  description: string;
  requirements: string;
  postingDate: string;
  closingDate: string;
  isActive: boolean;
  departmentId: number;
  departmentName: string;
  applicationCount: number;
}

@Injectable({ providedIn: 'root' })
export class JobPostingsService extends ApiClientBase {
  protected readonly endpoint = 'JobPostings';

  constructor(http: HttpClient) {
    super(http);
  }

  getJobPostings(activeOnly = true): Observable<JobPosting[]> {
    return this.http.get<JobPosting[]>(this.url(), { params: this.params({ activeOnly }) });
  }

  createJobPosting(dto: CreateJobPosting): Observable<void> {
    return this.http.post<void>(this.url(), dto);
  }

  closeJobPosting(id: number): Observable<void> {
    return this.http.patch<void>(this.url(`/${id}/close`), null);
  }

  getJobPosting(id: number): Observable<JobPosting> {
    return this.http.get<JobPosting>(this.url(`/${id}`));
  }

  updateJobPosting(id: number, dto: CreateJobPosting): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), dto);
  }

  deleteJobPosting(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}