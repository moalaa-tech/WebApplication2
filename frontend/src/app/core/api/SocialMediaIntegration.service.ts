import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export type SocialMediaPlatform = 'Facebook' | 'Twitter' | 'LinkedIn' | 'Instagram';
export type PostStatus = 'Draft' | 'Scheduled' | 'Published' | 'Failed';

export interface SocialMediaPostDto {
  id: number;
  content: string;
  scheduledTime: string;
  platform: SocialMediaPlatform;
  status: PostStatus;
}

@Injectable({ providedIn: 'root' })
export class SocialMediaIntegrationService extends ApiClientBase {
  protected readonly endpoint = 'SocialMediaIntegration';

  constructor(http: HttpClient) {
    super(http);
  }

  getSocialMediaPosts(): Observable<SocialMediaPostDto[]> {
    return this.http.get<SocialMediaPostDto[]>(this.url());
  }

  getSocialMediaPost(id: number): Observable<SocialMediaPostDto> {
    return this.http.get<SocialMediaPostDto>(this.url(`/${id}`));
  }

  createSocialMediaPost(post: SocialMediaPostDto): Observable<SocialMediaPostDto> {
    return this.http.post<SocialMediaPostDto>(this.url(), post);
  }

  updateSocialMediaPost(id: number, post: SocialMediaPostDto): Observable<SocialMediaPostDto> {
    return this.http.put<SocialMediaPostDto>(this.url(`/${id}`), post);
  }

  deleteSocialMediaPost(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  scheduleSocialMediaPost(id: number): Observable<void> {
    return this.http.post<void>(this.url(`/Schedule/${id}`), null);
  }
}