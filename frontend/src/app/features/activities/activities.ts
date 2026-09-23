import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ActivitiesService, Activity } from '../../core/api/Activities.service';

@Component({
  selector: 'app-activities',
  styleUrl: '../shared/list.css',
  templateUrl: './activities.html',
})
export class ActivitiesComponent implements OnInit {
  private readonly activitiesService = inject(ActivitiesService);

  readonly query = signal('');
  readonly activities = signal<Activity[]>([]);
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
      const q = this.query().trim() || undefined;
      const result = await firstValueFrom(this.activitiesService.getActivities(undefined, q));
      this.activities.set(result.activities);
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
      await firstValueFrom(this.activitiesService.deleteActivity(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}
