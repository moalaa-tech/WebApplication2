import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { QuoteListPage, QuotesService } from '../../core/api/Quotes.service';

@Component({
  selector: 'app-quotes',
  styleUrl: '../shared/list.css',
  templateUrl: './quotes.html',
})
export class QuotesComponent implements OnInit {
  private readonly quotesService = inject(QuotesService);

  readonly query = signal('');
  readonly quotesPage = signal<QuoteListPage | null>(null);
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
      this.quotesPage.set(await firstValueFrom(this.quotesService.getQuotes(undefined, q)));
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
      await firstValueFrom(this.quotesService.deleteQuote(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}