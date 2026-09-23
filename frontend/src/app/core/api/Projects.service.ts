import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ProjectDetails {
  id: number;
  projectCode: string;
  name: string;
  description: string | null;
  customerName: string | null;
  customerId: number;
  startDate: string;
  endDate: string | null;
  status: number;
  budget: number;
  duration: string;
  budgetUtilization: string;
  budgetUtilizationPercentage: number;
  costSummary?: ProjectCostSummaryView | null;
  phases: PhaseView[];
  recentTimeEntries: TimeEntryView[];
  recentExpenses: ExpenseView[];
  recentNotes: ProjectNote[];
  importantDocuments: ProjectDocument[];
  showFinancialData: boolean;
  isActive: boolean;
  isCompleted: boolean;
  returnUrl: string | null;
}

export interface ProjectCreate {
  projectCode: string;
  name: string;
  description?: string | null;
  customerId: number;
  startDate: string;
  endDate?: string | null;
  status: number;
  budget: number;
  customers?: unknown;
}

export interface UpdateProject {
  projectCode: string;
  name: string;
  description?: string | null;
  customerId: number;
  startDate: string;
  endDate?: string | null;
  status: number;
  budget: number;
  id: number;
}

export interface ProjectCostSummary {
  projectId: number;
  projectName: string;
  projectCode: string;
  budget: number;
  totalEstimatedCost: number;
  totalActualCost: number;
  variance: number;
  variancePercentage: number;
  laborCost: number;
  expenseCost: number;
  totalCost: number;
  totalHours: number;
  estimatedHours: number;
  hoursVariance: number;
  hoursVariancePercentage: number;
  isOverBudget: boolean;
  isOnBudget: boolean;
  isUnderBudget: boolean;
  startDate: string;
  endDate: string | null;
  estimatedCompletionDate: string | null;
  daysBehindSchedule: number;
  totalPhases: number;
  completedPhases: number;
  activePhases: number;
  plannedPhases: number;
}

export interface ProjectCostSummaryView {
  phaseId: number;
  phaseCode: string;
  phaseName: string;
  status: number;
  estimatedHours: number;
  actualHours: number;
  hoursVariance: number;
  estimatedCost: number;
  actualCost: number;
  costVariance: number;
  startDate: string | null;
  endDate: string | null;
  daysDuration: number | null;
  laborCost: number;
  materialCost: number;
  otherCost: number;
  completionPercentage: number;
}

export interface PhaseView {
  id: number;
  phaseCode: string;
  name: string;
  status: number;
  startDate: string | null;
  endDate: string | null;
  estimatedCost: number;
  actualCost: number;
  costVariance: number;
  progressStatus: string | null;
  completionPercentage: number;
}

export interface TimeEntryView {
  id: number;
  phaseName: string;
  userName: string;
  entryDate: string;
  hours: number;
  description: string;
  status: number;
  rate: number;
  total: number;
}

export interface ExpenseView {
  id: number;
  phaseName: string;
  expenseDate: string;
  description: string;
  category: number;
  amount: number;
  vendor: string;
  status: number;
  isBillable: boolean;
}

export interface ProjectNote {
  id: number;
  projectId: number;
  title: string;
  content: string;
  isImportant: boolean;
  category: string;
}

export interface ProjectDocument {
  id: number;
  name: string;
  documentType: string;
  uploadDate: string;
  fileType: string;
  fileSizeDisplay: string;
  isApproved: boolean;
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
export class ProjectsService extends ApiClientBase {
  protected readonly endpoint = 'Projects';

  constructor(http: HttpClient) {
    super(http);
  }

  getProjects(): Observable<ProjectDetails[]> {
    return this.http.get<ProjectDetails[]>(this.url());
  }

  getProject(id: number): Observable<ProjectDetails> {
    return this.http.get<ProjectDetails>(this.url(`/${id}`));
  }

  createProject(model: ProjectCreate): Observable<ProjectCreate> {
    return this.http.post<ProjectCreate>(this.url(), model);
  }

  updateProject(id: number, model: UpdateProject): Observable<UpdateProject> {
    return this.http.put<UpdateProject>(this.url(`/${id}`), model);
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getProjectCostSummary(id: number): Observable<ProjectCostSummary> {
    return this.http.get<ProjectCostSummary>(this.url(`/${id}/cost-summary`));
  }

  getProjectTimeEntries(id: number, fromDate?: string, toDate?: string): Observable<TimeEntry[]> {
    return this.http.get<TimeEntry[]>(this.url(`/${id}/time-entries`), {
      params: this.params({ fromDate, toDate }),
    });
  }

  getProjectExpenses(id: number, fromDate?: string, toDate?: string): Observable<PhaseExpense[]> {
    return this.http.get<PhaseExpense[]>(this.url(`/${id}/expenses`), {
      params: this.params({ fromDate, toDate }),
    });
  }
}