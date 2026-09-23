import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { PurchaseOrder, PurchaseOrdersService } from '../../core/api/PurchaseOrders.service';

@Component({
  selector: 'app-purchase-orders',
  styleUrl: '../shared/list.css',
  templateUrl: './purchase-orders.html',
})
export class PurchaseOrdersComponent implements OnInit {
  private readonly purchaseOrdersService = inject(PurchaseOrdersService);

  readonly purchaseOrders = signal<PurchaseOrder[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);
  readonly verifyNumber = signal('');
  readonly verifyResult = signal<string | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.purchaseOrders.set(await firstValueFrom(this.purchaseOrdersService.getPurchaseOrders()));
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
      await firstValueFrom(this.purchaseOrdersService.deletePurchaseOrder(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }

  async verify(): Promise<void> {
    const poNumber = this.verifyNumber().trim();
    if (!poNumber) {
      return;
    }
    this.error.set('');
    this.verifyResult.set(null);
    try {
      const exists = await firstValueFrom(this.purchaseOrdersService.verifyPONumber(poNumber));
      this.verifyResult.set(exists ? 'PO number exists.' : 'PO number not found.');
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    }
  }
}
