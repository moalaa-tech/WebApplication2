import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ReconciliationsService, Reconciliation } from '../../core/api/Reconciliations.service';

@Component({
  selector: 'app-reconciliations',
  styleUrl: '../shared/list.css',
  templateUrl: './reconciliations.html',
})
export class ReconciliationsComponent implements OnInit {
  private readonly reconciliationsService = inject(ReconciliationsService);

  readonly reconciliations = signal<Reconciliation[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.reconciliations.set(await firstValueFrom(this.reconciliationsService.getReconciliations()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}