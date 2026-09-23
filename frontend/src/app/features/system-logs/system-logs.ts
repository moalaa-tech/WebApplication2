import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { SystemLogsService, LogIndex, Log } from '../../core/api/SystemLogs.service';

@Component({
  selector: 'app-system-logs',
  styleUrl: '../shared/list.css',
  templateUrl: './system-logs.html',
})
export class SystemLogsComponent implements OnInit {
  private readonly systemLogsService = inject(SystemLogsService);

  readonly search = signal('');
  readonly page = signal(1);
  readonly pageSize = signal(20);
  readonly index = signal<LogIndex | null>(null);
  readonly logs = signal<Log[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const index = await firstValueFrom(
        this.systemLogsService.getSystemLogs({
          page: this.page(),
          pageSize: this.pageSize(),
          search: this.search().trim() || undefined,
        }),
      );
      this.index.set(index);
      this.logs.set(index.logs);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async goTo(page: number): Promise<void> {
    const max = this.index()?.totalPages ?? 1;
    const next = Math.max(1, Math.min(page, max));
    if (next === this.page()) {
      return;
    }
    this.page.set(next);
    await this.load();
  }

  async onPageChange(value: unknown): Promise<void> {
    const page = Number(value);
    if (!Number.isInteger(page) || page < 1) {
      return;
    }
    await this.goTo(page);
  }

  async onPageSizeChange(value: unknown): Promise<void> {
    const size = Number(value);
    if (!Number.isInteger(size) || size < 1) {
      return;
    }
    this.pageSize.set(size);
    this.page.set(1);
    await this.load();
  }
}