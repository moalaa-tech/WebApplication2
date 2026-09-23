import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Customer, CustomerService } from '../../core/api/Customer.service';

@Component({
  selector: 'app-customer',
  styleUrl: '../shared/list.css',
  templateUrl: './customer.html',
})
export class CustomerComponent implements OnInit {
  private readonly customerService = inject(CustomerService);

  readonly page = signal(1);
  readonly customers = signal<Customer[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  onPageChange(value: string): void {
    const parsed = Number(value);
    if (!Number.isInteger(parsed) || parsed < 1) {
      return;
    }
    this.page.set(parsed);
    void this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.customers.set(await firstValueFrom(this.customerService.getCustomers(this.page())));
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
      await firstValueFrom(this.customerService.deleteCustomer(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}