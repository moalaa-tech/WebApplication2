import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { WarehouseDto, WarehouseService } from '../../core/api/Warehouse.service';

@Component({
  selector: 'app-warehouse',
  styleUrl: '../shared/list.css',
  templateUrl: './warehouse.html',
})
export class WarehouseComponent implements OnInit {
  private readonly warehouseService = inject(WarehouseService);

  readonly warehouses = signal<WarehouseDto[]>([]);
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
      this.warehouses.set(await firstValueFrom(this.warehouseService.getWarehouses()));
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
      await firstValueFrom(this.warehouseService.deleteWarehouse(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}