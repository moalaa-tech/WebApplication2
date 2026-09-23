import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { BankTransaction, BankTransactionsService } from '../../core/api/BankTransactions.service';

@Component({
  selector: 'app-bank-transactions',
  styleUrl: '../shared/list.css',
  templateUrl: './bank-transactions.html',
})
export class BankTransactionsComponent implements OnInit {
  private readonly bankTransactionsService = inject(BankTransactionsService);

  readonly from = signal('');
  readonly to = signal('');
  readonly type = signal('');
  readonly transactions = signal<BankTransaction[]>([]);
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
      const from = this.from().trim() || null;
      const to = this.to().trim() || null;
      const raw = this.type().trim();
      const parsed = Number(raw);
      const type = raw === '' || Number.isNaN(parsed) ? null : parsed;
      this.transactions.set(
        await firstValueFrom(this.bankTransactionsService.filterBankTransactions(from, to, type)),
      );
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
      await firstValueFrom(this.bankTransactionsService.deleteBankTransaction(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}