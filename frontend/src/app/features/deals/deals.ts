import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Deal, DealsService } from '../../core/api/Deals.service';

@Component({
  selector: 'app-deals',
  styleUrl: '../shared/list.css',
  templateUrl: './deals.html',
})
export class DealsComponent implements OnInit {
  private readonly dealsService = inject(DealsService);

  readonly deals = signal<Deal[]>([]);
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
      this.deals.set(await firstValueFrom(this.dealsService.getDeals()));
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
      await firstValueFrom(this.dealsService.deleteDeal(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}