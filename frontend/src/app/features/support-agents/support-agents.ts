import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { SupportAgent, SupportAgentsService } from '../../core/api/SupportAgents.service';

@Component({
  selector: 'app-support-agents',
  styleUrl: '../shared/list.css',
  templateUrl: './support-agents.html',
})
export class SupportAgentsComponent implements OnInit {
  private readonly supportAgentsService = inject(SupportAgentsService);

  readonly supportAgents = signal<SupportAgent[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly togglingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.supportAgents.set(await firstValueFrom(this.supportAgentsService.getSupportAgents()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async toggle(id: number, isActive: boolean): Promise<void> {
    this.togglingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.supportAgentsService.toggleStatus(id, !isActive));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.togglingId.set(null);
    }
  }
}