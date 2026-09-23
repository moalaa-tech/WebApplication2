import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { InventoryService, ProductDto } from '../../core/api/Inventory.service';

@Component({
  selector: 'app-inventory',
  styleUrl: '../shared/list.css',
  templateUrl: './inventory.html',
})
export class InventoryComponent implements OnInit {
  private readonly inventoryService = inject(InventoryService);

  readonly pageIndex = signal(1);
  readonly products = signal<ProductDto[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  onPageInput(raw: string): void {
    this.pageIndex.set(Number(raw) || 1);
    void this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.products.set(
        await firstValueFrom(this.inventoryService.getInventorys(this.pageIndex())),
      );
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}