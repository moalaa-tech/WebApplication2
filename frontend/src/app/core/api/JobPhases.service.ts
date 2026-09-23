import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface JobPhase {
  id: number;
  projectId: number;
  projectName: string;
  projectCode: string;
  phaseCode: string;
  name: string;
  description: string | null;
  estimatedHours: number;
  estimatedCost: number;
  actualHours: number;
  actualCost: number;
  hoursVariance: number;
  costVariance: number;
  hoursVariancePercentage: number;
  costVariancePercentage: number;
  status: string | null;
  isCompleted: boolean;
  isActive: boolean;
  isBehindSchedule: boolean;
  startDate: string | null;
  endDate: string | null;
  estimatedEndDate: string | null;
  daysBehind: number;
  timeEntries: TimeEntry[];
  expenses: PhaseExpense[];
  completionPercentage: number;
  progressStatus: string | null;
  isApproved: boolean;
  approvedDate: string | null;
  approvedBy: string | null;
}

export interface CreateJobPhase {
  projectId: number;
  phaseCode: string;
  name: string;
  description?: string | null;
  estimatedHours: number;
  estimatedCost: number;
  startDate?: string | null;
  endDate?: string | null;
  status: number;
  projectStartDate: string;
  projectEndDate?: string | null;
}

export interface UpdateJobPhase {
  id: number;
  phaseCode: string;
  name: string;
  description?: string | null;
  estimatedHours: number;
  estimatedCost: number;
  startDate?: string | null;
  endDate?: string | null;
  status: number;
  createdDate: string;
  createdBy: string;
  modifiedDate?: string | null;
  modifiedBy: string;
}

export interface PhaseCostSummary {
  phaseId: number;
  phaseName: string;
  phaseCode: string;
  projectId: number;
  projectName: string;
  estimatedHours: number;
  estimatedCost: number;
  actualHours: number;
  actualCost: number;
  laborCost: number;
  expenseCost: number;
  hoursVariance: number;
  costVariance: number;
  hoursVariancePercentage: number;
  costVariancePercentage: number;
  isOverBudget: boolean;
  isUnderBudget: boolean;
  isOnBudget: boolean;
  startDate: string | null;
  endDate: string | null;
  estimatedEndDate: string | null;
  daysBehindSchedule: number;
  variance: number;
}

export interface TimeEntry {
  id: number;
  jobPhaseId: number;
  phaseName: string;
  userId: string;
  userName: string;
  userEmail: string;
  entryDate: string;
  hours: number;
  description: string;
  isBillable: boolean;
  rate: number;
  totalCost: number;
  status: string | null;
  isApproved: boolean;
  createdDate: string;
  modifiedDate: string | null;
}

export interface PhaseExpense {
  id: number;
  jobPhaseId: number;
  phaseName: string;
  description: string;
  expenseDate: string;
  amount: number;
  vendor: string;
  receiptNumber: string;
  vendorInvoiceNumber: string;
  category: string | null;
  isBillable: boolean;
  status: string | null;
  isApproved: boolean;
  createdDate: string;
  modifiedDate: string | null;
  createdBy: string;
}

@Injectable({ providedIn: 'root' })
export class JobPhasesService extends ApiClientBase {
  protected readonly endpoint = 'JobPhases';

  constructor(http: HttpClient) {
    super(http);
  }

  getJobPhases(projectId: number): Observable<JobPhase[]> {
    return this.http.get<JobPhase[]>(this.url(), { params: this.params({ projectId }) });
  }

  getJobPhase(projectId: number, id: number): Observable<JobPhase> {
    return this.http.get<JobPhase>(this.url(`/${id}`), { params: this.params({ projectId }) });
  }

  createJobPhase(projectId: number, model: CreateJobPhase): Observable<CreateJobPhase> {
    return this.http.post<CreateJobPhase>(this.url(), model, { params: this.params({ projectId }) });
  }

  updateJobPhase(projectId: number, id: number, model: UpdateJobPhase): Observable<UpdateJobPhase> {
    return this.http.put<UpdateJobPhase>(this.url(`/${id}`), model, { params: this.params({ projectId }) });
  }

  deleteJobPhase(projectId: number, id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`), { params: this.params({ projectId }) });
  }

  getJobPhaseCostSummary(projectId: number, id: number): Observable<PhaseCostSummary> {
    return this.http.get<PhaseCostSummary>(this.url(`/${id}/cost-summary`), {
      params: this.params({ projectId }),
    });
  }

  getJobPhaseTimeEntries(projectId: number, id: number, fromDate?: string, toDate?: string): Observable<TimeEntry[]> {
    return this.http.get<TimeEntry[]>(this.url(`/${id}/time-entries`), {
      params: this.params({ projectId, fromDate, toDate }),
    });
  }

  getJobPhaseExpenses(projectId: number, id: number, fromDate?: string, toDate?: string): Observable<PhaseExpense[]> {
    return this.http.get<PhaseExpense[]>(this.url(`/${id}/expenses`), {
      params: this.params({ projectId, fromDate, toDate }),
    });
  }
}