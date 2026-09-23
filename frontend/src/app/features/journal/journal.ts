import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { JournalEntry, JournalService } from '../../core/api/Journal.service';

@Component({
  selector: 'app-journal',
  styleUrl: '../shared/list.css',
  templateUrl: './journal.html',
})
export class JournalComponent implements OnInit {
  private readonly journalService = inject(JournalService);

  readonly fromDate = signal('');
  readonly toDate = signal('');
  readonly entries = signal<JournalEntry[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly postingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const from = this.fromDate().trim() || null;
      const to = this.toDate().trim() || null;
      const result = await firstValueFrom(this.journalService.getJournals(from, to, null));
      this.entries.set(result.entries);
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
      await firstValueFrom(this.journalService.postJournalEntry(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.postingId.set(null);
    }
  }
}