import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { DemandPlanListItem, DemandPlansService } from '../../core/api/DemandPlans.service';

@Component({
  selector: 'app-demand-plans',
  styleUrl: '../shared/list.css',
  templateUrl: './demand-plans.html',
})
export class DemandPlansComponent implements OnInit {
  private readonly demandPlansService = inject(DemandPlansService);

  readonly demandPlans = signal<DemandPlanListItem[]>([]);
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
      this.demandPlans.set(await firstValueFrom(this.demandPlansService.getDemandPlans()));
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
      await firstValueFrom(this.demandPlansService.deleteDemandPlan(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}