import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Lead, SalesService } from '../../core/api/Sales.service';

@Component({
  selector: 'app-sales',
  styleUrl: '../shared/list.css',
  templateUrl: './sales.html',
})
export class SalesComponent implements OnInit {
  private readonly salesService = inject(SalesService);

  readonly leads = signal<Lead[]>([]);
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
      this.leads.set(await firstValueFrom(this.salesService.getSales()));
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
      await firstValueFrom(this.salesService.deleteSale(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}