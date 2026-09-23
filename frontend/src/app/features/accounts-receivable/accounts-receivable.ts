import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AccountsReceivable, AccountsReceivableService } from '../../core/api/AccountsReceivable.service';

@Component({
  selector: 'app-accounts-receivable',
  styleUrl: '../shared/list.css',
  templateUrl: './accounts-receivable.html',
})
export class AccountsReceivableComponent implements OnInit {
  private readonly accountsReceivableService = inject(AccountsReceivableService);

  readonly accountsReceivables = signal<AccountsReceivable[]>([]);
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
      this.accountsReceivables.set(
        await firstValueFrom(this.accountsReceivableService.getAccountsReceivables()),
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
      await firstValueFrom(this.accountsReceivableService.deleteAccountsReceivable(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}