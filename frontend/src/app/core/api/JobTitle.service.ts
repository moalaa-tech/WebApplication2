import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface SelectListItem {
  text: string;
  value: string;
  disabled?: boolean;
  selected?: boolean;
}

export interface JobTitle {
  id: number;
  title: string;
  departmentName: string;
  departments: SelectListItem[];
}

export interface CreateJobTitle {
  title: string;
  departmentId: number;
  departments?: SelectListItem[];
}

export interface UpdateJobTitle {
  id: number;
  title: string;
  departmentId: number;
  departments?: SelectListItem[];
}

@Injectable({ providedIn: 'root' })
export class JobTitleService extends ApiClientBase {
  protected readonly endpoint = 'JobTitle';

  constructor(http: HttpClient) {
    super(http);
  }

  getJobTitles(): Observable<JobTitle[]> {
    return this.http.get<JobTitle[]>(this.url());
  }

  createJobTitle(model: CreateJobTitle): Observable<void> {
    return this.http.post<void>(this.url(), model);
  }

  updateJobTitle(id: number, model: UpdateJobTitle): Observable<void> {
    return this.http.put<void>(this.url(`/${id}`), model);
  }

  deleteJobTitle(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}