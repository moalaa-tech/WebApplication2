import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AccountsPayableService, Invoice } from '../../core/api/AccountsPayable.service';

@Component({
  selector: 'app-accounts-payable',
  styleUrl: '../shared/list.css',
  templateUrl: './accounts-payable.html',
})
export class AccountsPayableComponent implements OnInit {
  private readonly accountsPayableService = inject(AccountsPayableService);

  readonly invoices = signal<Invoice[]>([]);
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
      this.invoices.set(await firstValueFrom(this.accountsPayableService.getInvoices()));
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
      await firstValueFrom(this.accountsPayableService.deleteInvoice(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}