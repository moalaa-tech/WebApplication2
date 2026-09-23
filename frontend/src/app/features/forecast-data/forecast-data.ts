import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ForecastData, ForecastDataService } from '../../core/api/ForecastData.service';

@Component({
  selector: 'app-forecast-data',
  styleUrl: '../shared/list.css',
  templateUrl: './forecast-data.html',
})
export class ForecastDataComponent implements OnInit {
  private readonly forecastDataService = inject(ForecastDataService);

  readonly selectedId = signal<number | null>(null);
  readonly items = signal<ForecastData[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  onIdInput(value: string): void {
    const trimmed = value.trim();
    if (trimmed === '') {
      this.selectedId.set(null);
      void this.load();
      return;
    }
    const parsed = Number(trimmed);
    if (!Number.isInteger(parsed) || parsed <= 0) {
      return;
    }
    this.selectedId.set(parsed);
    void this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const id = this.selectedId();
      if (id === null) {
        this.items.set(await firstValueFrom(this.forecastDataService.getForecastData()));
      } else {
        const item = await firstValueFrom(this.forecastDataService.getForecastData(id));
        this.items.set([item]);
      }
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async remove(id: number): Promise<void> {
    this.removingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.forecastDataService.deleteForecastData(id));
      this.selectedId.set(null);
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}