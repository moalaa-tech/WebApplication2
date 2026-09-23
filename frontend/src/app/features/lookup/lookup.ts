import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { City, Country, LookupService, State } from '../../core/api/Lookup.service';

@Component({
  selector: 'app-lookup',
  styleUrl: '../shared/list.css',
  templateUrl: './lookup.html',
})
export class LookupComponent implements OnInit {
  private readonly lookupService = inject(LookupService);

  readonly countries = signal<Country[]>([]);
  readonly states = signal<State[]>([]);
  readonly cities = signal<City[]>([]);
  readonly countryId = signal<number | null>(null);
  readonly stateId = signal<number | null>(null);
  readonly cityId = signal<number | null>(null);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);
  readonly filteredStates = computed(() => {
    const countryId = this.countryId();
    if (countryId === null) {
      return this.states();
    }
    return this.states().filter((state) => state.countryId === countryId);
  });

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.countries.set(await firstValueFrom(this.lookupService.getCountries()));
      this.states.set(await firstValueFrom(this.lookupService.getStates()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  onCountryChange(value: string): void {
    this.countryId.set(value ? Number(value) : null);
    this.stateId.set(null);
    this.cityId.set(null);
    this.cities.set([]);
  }

  async onStateChange(value: string): Promise<void> {
    const id = value ? Number(value) : null;
    this.stateId.set(id);
    this.cityId.set(null);
    this.cities.set([]);
    if (id !== null) {
      this.error.set('');
      try {
        this.cities.set(await firstValueFrom(this.lookupService.getCitiesByStateId(id)));
      } catch (e) {
        this.error.set(e instanceof Error ? e.message : String(e));
      }
    }
  }

  async removeCountry(id: number): Promise<void> {
    this.removingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.lookupService.deleteCountry(id));
      this.countries.set(await firstValueFrom(this.lookupService.getCountries()));
      if (this.countryId() === id) {
        this.countryId.set(null);
        this.stateId.set(null);
        this.cityId.set(null);
        this.cities.set([]);
      }
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }

  async removeState(id: number): Promise<void> {
    this.removingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.lookupService.deleteState(id));
      this.states.set(await firstValueFrom(this.lookupService.getStates()));
      if (this.stateId() === id) {
        this.stateId.set(null);
        this.cityId.set(null);
        this.cities.set([]);
      }
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}