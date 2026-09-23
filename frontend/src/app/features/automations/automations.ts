import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AutomationsService, Automation } from '../../core/api/Automations.service';

@Component({
  selector: 'app-automations',
  styleUrl: '../shared/list.css',
  templateUrl: './automations.html',
})
export class AutomationsComponent implements OnInit {
  private readonly automationsService = inject(AutomationsService);

  readonly automations = signal<Automation[]>([]);
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
      this.automations.set(await firstValueFrom(this.automationsService.getAutomations()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async toggle(id: number): Promise<void> {
    this.togglingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.automationsService.toggleAutomationStatus(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.togglingId.set(null);
    }
  }
}