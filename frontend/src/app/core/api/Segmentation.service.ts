import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Segment {
  id: number;
  name: string;
  description: string;
  criteria: string;
  createdDate: string;
  lastModifiedDate?: string | null;
  isActive: boolean;
  segmentType: string;
}

export interface SegmentCreateViewModel {
  name: string;
  description: string;
  criteria: string;
  isActive: boolean;
  segmentType: string;
}

export interface SegmentEditViewModel {
  id: number;
  name: string;
  description: string;
  criteria: string;
  isActive: boolean;
  segmentType: string;
}

@Injectable({ providedIn: 'root' })
export class SegmentationService extends ApiClientBase {
  protected readonly endpoint = 'Segmentation';

  constructor(http: HttpClient) {
    super(http);
  }

  getSegments(): Observable<Segment[]> {
    return this.http.get<Segment[]>(this.url());
  }

  getSegment(id: number): Observable<Segment> {
    return this.http.get<Segment>(this.url(`/${id}`));
  }

  createSegment(viewModel: SegmentCreateViewModel): Observable<SegmentCreateViewModel> {
    return this.http.post<SegmentCreateViewModel>(this.url(), viewModel);
  }

  updateSegment(id: number, viewModel: SegmentEditViewModel): Observable<SegmentEditViewModel> {
    return this.http.put<SegmentEditViewModel>(this.url(`/${id}`), viewModel);
  }

  deleteSegment(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }
}