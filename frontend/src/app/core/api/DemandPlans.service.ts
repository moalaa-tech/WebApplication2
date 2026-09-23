import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Option {
  id: number;
  name: string;
}

export interface DemandPlan {
  id: number;
  planName: string;
  startDate: string;
  endDate: string;
  status: string;
  notes: string;
  items?: DemandPlanItem[];
}

export interface DemandPlanListItem {
  id: number;
  planName: string;
  startDate: string;
  endDate: string;
  status: string;
  notes: string;
}

export interface DemandPlanItem {
  id: number;
  demandPlanId: number;
  productId: number;
  period: string;
  quantity: number;
  price?: number | null;
  notes: string;
  productCode?: string;
  productName?: string;
  productCategory?: string;
  createdBy: string;
  createdDate: string;
  updatedBy?: string;
  updatedDate?: string | null;
  version: number;
  isApproved: boolean;
  approvalDate?: string | null;
  approvedBy?: string;
}

export interface DemandPlanItemCreate {
  demandPlanId: number;
  productId: number;
  period: string;
  quantity: number;
  price?: number | null;
  notes: string;
  createdBy: string;
}

export interface DemandPlanCreate {
  planName: string;
  description: string;
  startDate: string;
  endDate: string;
  planTypeId: number;
  businessUnitId?: number | null;
  regionId?: number | null;
  isActive: boolean;
  planTypeOptions?: Option[];
  businessUnitOptions?: Option[];
  regionOptions?: Option[];
}

export interface DemandPlanUpdate {
  id: number;
  planName: string;
  description: string;
  startDate: string;
  endDate: string;
  planTypeId: number;
  businessUnitId?: number | null;
  regionId?: number | null;
  isActive: boolean;
  versionNumber: number;
  planTypeOptions?: Option[];
  businessUnitOptions?: Option[];
  regionOptions?: Option[];
}

export interface SupplyChainDashboard {
  totalPlans: number;
  activePlans: number;
  completedPlans: number;
  totalEstimatedCost: number;
  totalItems: number;
  procuredItems: number;
  recentPlans?: DemandPlan[];
  highPriorityItems?: DemandPlanItem[];
}

export interface OperationResult {
  success: boolean;
  message: string;
}

@Injectable({ providedIn: 'root' })
export class DemandPlansService extends ApiClientBase {
  protected readonly endpoint = 'DemandPlans';

  constructor(http: HttpClient) {
    super(http);
  }

  getDemandPlans(): Observable<DemandPlanListItem[]> {
    return this.http.get<DemandPlanListItem[]>(this.url());
  }

  getDemandPlan(id: number): Observable<DemandPlan> {
    return this.http.get<DemandPlan>(this.url(`/${id}`));
  }

  createDemandPlan(model: DemandPlanCreate): Observable<DemandPlanCreate> {
    return this.http.post<DemandPlanCreate>(this.url(), model);
  }

  updateDemandPlan(id: number, model: DemandPlanUpdate): Observable<DemandPlanUpdate> {
    return this.http.put<DemandPlanUpdate>(this.url(`/${id}`), model);
  }

  deleteDemandPlan(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  getDashboard(): Observable<SupplyChainDashboard> {
    return this.http.get<SupplyChainDashboard>(this.url('/Dashboard'));
  }

  addItem(planId: number, item: DemandPlanItemCreate): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.url('/AddItem'), item, {
      params: this.params({ planId }),
    });
  }

  updateItemStatus(itemId: number, isProcured: boolean): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.url('/UpdateItemStatus'), null, {
      params: this.params({ itemId, isProcured }),
    });
  }

  generateForecast(planId: number): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.url('/GenerateForecast'), null, {
      params: this.params({ planId }),
    });
  }
}