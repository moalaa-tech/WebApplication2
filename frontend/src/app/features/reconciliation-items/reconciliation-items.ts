import { Component, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ReconciliationItem, ReconciliationItemsService } from '../../core/api/ReconciliationItems.service';

@Component({
  selector: 'app-reconciliation-items',
  styleUrl: '../shared/list.css',
  templateUrl: './reconciliation-items.html',
})
export class ReconciliationItemsComponent {
  private readonly reconciliationItemsService = inject(ReconciliationItemsService);

  readonly reconciliationId = signal(1);
  readonly items = signal<ReconciliationItem[]>([]);
  readonly loading = signal(false);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.items.set(
        await firstValueFrom(
          this.reconciliationItemsService.getReconciliationItems(this.reconciliationId()),
        ),
      );
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  setReconciliationId(value: string): void {
    const parsed = Number(value);
    this.reconciliationId.set(Number.isNaN(parsed) ? 1 : parsed);
    void this.load();
  }

  async remove(id: number): Promise<void> {
    this.removingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.reconciliationItemsService.deleteReconciliationItem(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}