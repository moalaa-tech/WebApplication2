import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

export interface ApiError {
  title?: string;
  status?: number;
  detail?: string;
  errors?: Record<string, string[]>;
}

/**
 * Base class for all typed API client services.
 *
 * Subclasses set the `endpoint` route fragment (e.g. 'Vendors') and expose
 * one method per controller action. URLs are built as
 * `${apiUrl}/api/${endpoint}[/${suffix}]` so requests go through the
 * development proxy (`proxy.conf.json`) in `ng serve`.
 */
@Injectable()
export abstract class ApiClientBase {
  protected abstract readonly endpoint: string;

  constructor(protected readonly http: HttpClient) {}

  protected url(suffix = ''): string {
    const sep = suffix && !suffix.startsWith('/') ? '/' : '';
    return `${environment.apiUrl}/api/${this.endpoint}${sep}${suffix}`;
  }

  protected params(
    args?: Record<string, string | number | boolean | null | undefined>,
  ): HttpParams {
    let p = new HttpParams();
    if (args) {
      for (const [key, value] of Object.entries(args)) {
        if (value !== null && value !== undefined) {
          p = p.set(key, String(value));
        }
      }
    }
    return p;
  }
}