import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Activity {
  id: number;
  type: number;
  typeName: string;
  subject: string;
  description: string;
  dueDate: string;
  completedDate?: string | null;
  status: number;
  statusName: string;
  leadId?: number | null;
  leadName: string;
  opportunityId?: number | null;
  opportunityName: string;
  dealId?: number | null;
  dealName: string;
  ownerUserId: string;
  ownerUserName: string;
  createdDate: string;
  isOverdue: boolean;
}

export interface SelectListItem {
  text: string;
  value?: string | null;
  selected: boolean;
  disabled: boolean;
}

export interface PaginatedActivities extends Array<Activity> {
  pageIndex: number;
  totalPages: number;
  totalCount: number;
  hasNextPage: boolean;
}

export interface PagingInfo {
  totalItems: number;
  itemsPerPage: number;
  currentPage: number;
  totalPages: number;
}

export interface ActivityFilter {
  searchTerm?: string | null;
  fromDate?: string | null;
  toDate?: string | null;
  ownerUserId: number;
  type?: number | null;
  status?: number | null;
}

export interface ActivityList {
  activities: PaginatedActivities;
  pagingInfo: PagingInfo;
  filter: ActivityFilter;
  users: SelectListItem[];
}

export interface CreateActivity {
  leadId?: number | null;
  opportunityId?: number | null;
  dealId?: number | null;
}

export interface UpdateActivity extends Activity {
  users?: SelectListItem[];
  types?: SelectListItem[];
  statuses?: SelectListItem[];
}

export interface CompleteActivity {
  id: number;
  subject: string;
  dueDate: string;
  outcomeNotes: string;
}

@Injectable({ providedIn: 'root' })
export class ActivitiesService extends ApiClientBase {
  protected readonly endpoint = 'Activities';

  constructor(http: HttpClient) {
    super(http);
  }

  getActivities(
    page?: number,
    searchTerm?: string,
    type?: number,
    status?: number,
    fromDate?: string,
    toDate?: string,
    ownerUserId?: string,
  ): Observable<ActivityList> {
    return this.http.get<ActivityList>(this.url(), {
      params: this.params({ page, searchTerm, type, status, fromDate, toDate, ownerUserId }),
    });
  }

  getUpcoming(daysAhead?: number): Observable<Activity[]> {
    return this.http.get<Activity[]>(this.url('/Upcoming'), { params: this.params({ daysAhead }) });
  }

  getActivity(id: number): Observable<Activity> {
    return this.http.get<Activity>(this.url(`/${id}`));
  }

  createActivity(dto: CreateActivity): Observable<CreateActivity | null> {
    return this.http.post<CreateActivity | null>(this.url(), dto);
  }

  updateActivity(id: number, dto: UpdateActivity): Observable<UpdateActivity | null> {
    return this.http.put<UpdateActivity | null>(this.url(`/${id}`), dto);
  }

  deleteActivity(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  completeActivity(id: number, dto: CompleteActivity): Observable<CompleteActivity | null> {
    return this.http.post<CompleteActivity | null>(this.url('/Complete'), dto, {
      params: this.params({ id }),
    });
  }
}
