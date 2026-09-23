import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { GeneralLedger, GeneralLedgerService } from '../../core/api/GeneralLedger.service';

@Component({
  selector: 'app-general-ledger',
  styleUrl: '../shared/list.css',
  templateUrl: './general-ledger.html',
})
export class GeneralLedgerComponent implements OnInit {
  private readonly generalLedgerService = inject(GeneralLedgerService);

  readonly ledgers = signal<GeneralLedger[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);
  readonly postingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.ledgers.set(await firstValueFrom(this.generalLedgerService.getGeneralLedgers()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async post(id: number): Promise<void> {
    this.postingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.generalLedgerService.postTransaction(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.postingId.set(null);
    }
  }

  async remove(id: number): Promise<void> {
    this.removingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.generalLedgerService.deleteGeneralLedger(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}