import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { BankAccount, BankAccountsService } from '../../core/api/BankAccounts.service';

@Component({
  selector: 'app-bank-accounts',
  styleUrl: '../shared/list.css',
  templateUrl: './bank-accounts.html',
})
export class BankAccountsComponent implements OnInit {
  private readonly bankAccountsService = inject(BankAccountsService);

  readonly accounts = signal<BankAccount[]>([]);
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
      this.accounts.set(await firstValueFrom(this.bankAccountsService.getBankAccounts()));
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
      await firstValueFrom(this.bankAccountsService.deleteBankAccount(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}