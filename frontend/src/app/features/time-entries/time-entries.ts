import { Component, inject, OnInit, signal } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { TimeEntriesService, TimeEntry } from '../../core/api/TimeEntries.service';

@Component({
  selector: 'app-time-entries',
  styleUrl: '../shared/list.css',
  templateUrl: './time-entries.html',
})
export class TimeEntriesComponent implements OnInit {
  private readonly timeEntriesService = inject(TimeEntriesService);

  readonly fromDate = signal('');
  readonly toDate = signal('');
  readonly timeEntries = signal<TimeEntry[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly busyId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  onDateChange(): void {
    void this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const from = this.fromDate() || undefined;
      const to = this.toDate() || undefined;
      this.timeEntries.set(await firstValueFrom(this.timeEntriesService.getTimeEntries(from, to)));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async approve(id: number): Promise<void> {
    await this.mutate(id, () => this.timeEntriesService.approveTimeEntry(id));
  }

  async reject(id: number): Promise<void> {
    await this.mutate(id, () => this.timeEntriesService.rejectTimeEntry(id));
  }

  async remove(id: number): Promise<void> {
    await this.mutate(id, () => this.timeEntriesService.deleteTimeEntry(id));
  }

  private async mutate(id: number, action: () => Observable<void>): Promise<void> {
    this.busyId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(action());
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busyId.set(null);
    }
  }
}