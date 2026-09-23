import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  AnalyticsROITrackingService,
  CampaignAnalyticsIndexViewModel,
} from '../../core/api/AnalyticsROITracking.service';

@Component({
  selector: 'app-analytics-roi-tracking',
  styleUrl: '../shared/list.css',
  templateUrl: './analytics-roi-tracking.html',
})
export class AnalyticsROITrackingComponent implements OnInit {
  private readonly analyticsService = inject(AnalyticsROITrackingService);

  readonly index = signal<CampaignAnalyticsIndexViewModel | null>(null);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.index.set(await firstValueFrom(this.analyticsService.getAnalytics()));
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
      await firstValueFrom(this.analyticsService.deleteAnalytics(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }

  channelEntries(): { key: string; value: number }[] {
    const current = this.index();
    if (!current) {
      return [];
    }
    return Object.entries(current.channelPerformance).map(([key, value]) => ({
      key,
      value,
    }));
  }
}