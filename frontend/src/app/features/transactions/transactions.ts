import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { TransactionsService } from '../../core/api/Transactions.service';

@Component({
  selector: 'app-transactions',
  styleUrl: '../shared/list.css',
  templateUrl: './transactions.html',
})
export class TransactionsComponent implements OnInit {
  private readonly transactionsService = inject(TransactionsService);

  readonly productId = signal('');
  readonly loaded = signal(false);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      await firstValueFrom(
        this.transactionsService.getTransactions(this.productId().trim() || undefined),
      );
      this.loaded.set(true);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}