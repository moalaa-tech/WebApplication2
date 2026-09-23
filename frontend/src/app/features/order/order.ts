import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { OrderDto, OrderService, OrderViewModel } from '../../core/api/Order.service';

@Component({
  selector: 'app-order',
  styleUrl: '../shared/list.css',
  templateUrl: './order.html',
})
export class OrderComponent implements OnInit {
  private readonly orderService = inject(OrderService);

  readonly query = signal('');
  readonly status = signal<number | null>(null);
  readonly dateFrom = signal('');
  readonly dateTo = signal('');
  readonly orders = signal<OrderDto[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly exportingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const model: OrderViewModel = {
        search: this.query().trim() || null,
        status: this.status(),
        dateFrom: this.dateFrom() || null,
        dateTo: this.dateTo() || null,
        pageIndex: 1,
        pageSize: 50,
      };
      const result = await firstValueFrom(this.orderService.getOrders(model));
      this.orders.set(result.model.result ?? []);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  onStatusChange(value: string): void {
    this.status.set(value === '' ? null : Number(value));
  }

  async exportPdf(id: number): Promise<void> {
    this.exportingId.set(id);
    this.error.set('');
    try {
      const blob = await firstValueFrom(this.orderService.exportPdf(id));
      const url = URL.createObjectURL(blob);
      const anchor = document.createElement('a');
      anchor.href = url;
      anchor.download = `order-${id}.pdf`;
      anchor.click();
      URL.revokeObjectURL(url);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.exportingId.set(null);
    }
  }
}