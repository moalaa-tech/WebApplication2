import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Country {
  id: number;
  name?: string | null;
  nameAr?: string | null;
  iSO2?: string | null;
  iSO3?: string | null;
}

export interface State {
  id: number;
  name: string;
  nameAr: string;
  countryId: number;
  countryName?: string | null;
  country?: Country | null;
}

export interface StateInput {
  id: string;
  name: string;
  nameAr: string;
  countryId: number;
  countryName?: string | null;
}

export interface City {
  id: number;
  name: string;
  nameAr: string;
  stateId: number;
  stateName?: string | null;
  state?: State | null;
}

@Injectable({ providedIn: 'root' })
export class LookupService extends ApiClientBase {
  protected readonly endpoint = 'Lookup';

  constructor(http: HttpClient) {
    super(http);
  }

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(this.url('/CountriesIndex'));
  }

  createCountry(dto: Country): Observable<void> {
    return this.http.post<void>(this.url('/CreateCountry'), dto);
  }

  updateCountry(dto: Country): Observable<void> {
    return this.http.put<void>(this.url('/EditCountry'), dto);
  }

  deleteCountry(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/DeleteCountry/${id}`));
  }

  getStates(): Observable<State[]> {
    return this.http.get<State[]>(this.url('/StatesIndex'));
  }

  createState(dto: StateInput): Observable<void> {
    return this.http.post<void>(this.url('/CreateState'), dto);
  }

  updateState(dto: StateInput): Observable<void> {
    return this.http.put<void>(this.url('/EditState'), dto);
  }

  deleteState(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/DeleteState/${id}`));
  }

  getCitiesByStateId(stateId: number): Observable<City[]> {
    return this.http.get<City[]>(this.url(`/GetCitiesByStateId/${stateId}`));
  }
}
