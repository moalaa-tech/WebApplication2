import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Ticket, TicketsService } from '../../core/api/Tickets.service';

@Component({
  selector: 'app-tickets',
  styleUrl: '../shared/list.css',
  templateUrl: './tickets.html',
})
export class TicketsComponent implements OnInit {
  private readonly ticketsService = inject(TicketsService);

  readonly tickets = signal<Ticket[]>([]);
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
      this.tickets.set(await firstValueFrom(this.ticketsService.getTickets()));
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
      await firstValueFrom(this.ticketsService.deleteTicket(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}