import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface JobApplication {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  phone: string;
  resumePath: string;
  coverLetterPath: string;
  jobPostingId: number;
  jobTitle: string;
  status: string;
  statusName: string;
  applicationDate: string;
  interviewDate?: string | null;
  notes: string;
}

export interface CreateJobApplication {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  resumePath?: string | null;
  coverLetterPath?: string | null;
  jobPostingId: number;
  jobTitle: string;
  status: string;
  applicationDate: string;
  interviewDate?: string | null;
  notes?: string | null;
}

export interface UpdateJobApplication {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  resumePath?: string | null;
  coverLetterPath?: string | null;
  jobPostingId: number;
  jobTitle: string;
  status: string;
  applicationDate: string;
  interviewDate?: string | null;
  notes?: string | null;
}

@Injectable({ providedIn: 'root' })
export class JobApplicationsService extends ApiClientBase {
  protected readonly endpoint = 'JobApplications';

  constructor(http: HttpClient) {
    super(http);
  }

  getJobApplications(): Observable<JobApplication[]> {
    return this.http.get<JobApplication[]>(this.url());
  }

  submitApplication(dto: CreateJobApplication, resume?: File, coverLetter?: File): Observable<void> {
    const formData = new FormData();
    formData.append('firstName', dto.firstName);
    formData.append('lastName', dto.lastName);
    formData.append('email', dto.email);
    formData.append('phone', dto.phone);
    formData.append('resumePath', dto.resumePath ?? '');
    formData.append('coverLetterPath', dto.coverLetterPath ?? '');
    formData.append('jobPostingId', String(dto.jobPostingId));
    formData.append('jobTitle', dto.jobTitle);
    formData.append('status', dto.status);
    formData.append('applicationDate', dto.applicationDate);
    if (dto.interviewDate) {
      formData.append('interviewDate', dto.interviewDate);
    }
    if (dto.notes) {
      formData.append('notes', dto.notes);
    }
    if (resume) {
      formData.append('resume', resume);
    }
    if (coverLetter) {
      formData.append('coverLetter', coverLetter);
    }
    return this.http.post<void>(this.url(), formData);
  }

  getApplicationsByJobPosting(jobPostingId: number): Observable<JobApplication[]> {
    return this.http.get<JobApplication[]>(this.url(`/job/${jobPostingId}`));
  }

  updateStatus(id: number, dto: UpdateJobApplication): Observable<void> {
    return this.http.patch<void>(this.url(`/${id}/status`), dto);
  }

  getJobApplication(id: number): Observable<JobApplication> {
    return this.http.get<JobApplication>(this.url(`/${id}`));
  }

  downloadResume(id: number): Observable<Blob> {
    return this.http.get(this.url(`/DownloadResume/${id}`), { responseType: 'blob' });
  }

  downloadCoverLetter(id: number): Observable<Blob> {
    return this.http.get(this.url(`/DownloadCoverLetter/${id}`), { responseType: 'blob' });
  }

  getInterviewSchedule(id: number): Observable<UpdateJobApplication> {
    return this.http.get<UpdateJobApplication>(this.url(`/ScheduleInterview/${id}`));
  }

  scheduleInterview(id: number, dto: UpdateJobApplication): Observable<void> {
    return this.http.post<void>(this.url(`/ScheduleInterview/${id}`), dto);
  }
}