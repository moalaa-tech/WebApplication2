import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { City, Country, LocationService, State } from '../../core/api/Location.service';

@Component({
  selector: 'app-location',
  styleUrl: '../shared/list.css',
  templateUrl: './location.html',
})
export class LocationComponent implements OnInit {
  private readonly locationService = inject(LocationService);

  readonly countries = signal<Country[]>([]);
  readonly states = signal<State[]>([]);
  readonly cities = signal<City[]>([]);
  readonly selectedCountryId = signal<number | null>(null);
  readonly selectedStateId = signal<number | null>(null);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const lookup = await firstValueFrom(this.locationService.getLocations());
      this.countries.set(lookup.countries);
      this.states.set(lookup.states);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async onCountryChange(raw: string): Promise<void> {
    const id = raw === '' ? null : Number(raw);
    this.selectedCountryId.set(id);
    this.selectedStateId.set(null);
    this.cities.set([]);
    this.error.set('');
    if (id === null) {
      this.states.set([]);
      return;
    }
    this.loading.set(true);
    try {
      this.states.set(await firstValueFrom(this.locationService.getStatesByCountry(id)));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async onStateChange(raw: string): Promise<void> {
    const id = raw === '' ? null : Number(raw);
    this.selectedStateId.set(id);
    this.cities.set([]);
    this.error.set('');
    if (id === null) {
      return;
    }
    this.loading.set(true);
    try {
      this.cities.set(await firstValueFrom(this.locationService.getCitiesByState(id)));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}