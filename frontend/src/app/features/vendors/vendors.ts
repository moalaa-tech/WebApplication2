import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Vendor, VendorsService } from '../../core/api/Vendors.service';

@Component({
  selector: 'app-vendors',
  styleUrl: '../shared/list.css',
  templateUrl: './vendors.html',
})
export class VendorsComponent implements OnInit {
  private readonly vendorsService = inject(VendorsService);

  readonly query = signal('');
  readonly vendors = signal<Vendor[]>([]);
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
      const q = this.query().trim() || undefined;
      this.vendors.set(await firstValueFrom(this.vendorsService.getVendors(q)));
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
      await firstValueFrom(this.vendorsService.deleteVendor(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}