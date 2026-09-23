import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { HealthReport, HealthService } from '../../core/api/Health.service';

@Component({
  selector: 'app-health',
  styleUrl: '../shared/list.css',
  templateUrl: './health.html',
})
export class HealthComponent implements OnInit {
  private readonly healthService = inject(HealthService);

  readonly report = signal<HealthReport | null>(null);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.report.set(await firstValueFrom(this.healthService.getHealth()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}