import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { SelectListItem, StockService } from '../../core/api/Stock.service';

@Component({
  selector: 'app-stock',
  styleUrl: '../shared/list.css',
  templateUrl: './stock.html',
})
export class StockComponent implements OnInit {
  private readonly stockService = inject(StockService);

  readonly view = signal<'in' | 'out' | 'adjust'>('in');
  readonly items = signal<SelectListItem[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async setView(view: 'in' | 'out' | 'adjust'): Promise<void> {
    this.view.set(view);
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const list =
        this.view() === 'out'
          ? this.stockService.getStockOut()
          : this.view() === 'adjust'
            ? this.stockService.getStockAdjust()
            : this.stockService.getStockIn();
      this.items.set(await firstValueFrom(list));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}