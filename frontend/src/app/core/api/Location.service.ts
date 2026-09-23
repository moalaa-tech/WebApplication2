import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Country {
  id: number;
  name?: string | null;
  nameAr?: string | null;
  iso2?: string | null;
  iso3?: string | null;
}

export interface State {
  id: number;
  name: string;
  nameAr: string;
  countryId: number;
  countryName?: string | null;
  country?: Country | null;
}

export interface City {
  id: number;
  name: string;
  nameAr: string;
  stateId: number;
  stateName?: string | null;
  state?: State | null;
}

export interface LocationLookup {
  countries: Country[];
  states: State[];
}

@Injectable({ providedIn: 'root' })
export class LocationService extends ApiClientBase {
  protected readonly endpoint = 'Location';

  constructor(http: HttpClient) {
    super(http);
  }

  getLocations(): Observable<LocationLookup> {
    return this.http.get<LocationLookup>(this.url());
  }

  getStatesByCountry(countryId: number): Observable<State[]> {
    return this.http.get<State[]>(this.url('/GetStatesByCountry'), { params: this.params({ countryId }) });
  }

  getCitiesByState(stateId: number): Observable<City[]> {
    return this.http.get<City[]>(this.url('/GetCitiesByState'), { params: this.params({ stateId }) });
  }
}