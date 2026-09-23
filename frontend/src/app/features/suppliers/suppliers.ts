import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Supplier, SuppliersService } from '../../core/api/Suppliers.service';

@Component({
  selector: 'app-suppliers',
  styleUrl: '../shared/list.css',
  templateUrl: './suppliers.html',
})
export class SuppliersComponent implements OnInit {
  private readonly suppliersService = inject(SuppliersService);

  readonly suppliers = signal<Supplier[]>([]);
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
      this.suppliers.set(await firstValueFrom(this.suppliersService.getSuppliers()));
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
      await firstValueFrom(this.suppliersService.deleteSupplier(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}
