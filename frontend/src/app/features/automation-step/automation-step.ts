import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AutomationStepService, AutomationStep } from '../../core/api/AutomationStep.service';

@Component({
  selector: 'app-automation-step',
  styleUrl: '../shared/list.css',
  templateUrl: './automation-step.html',
})
export class AutomationStepComponent implements OnInit {
  private readonly automationStepService = inject(AutomationStepService);

  readonly steps = signal<AutomationStep[]>([]);
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
      this.steps.set(await firstValueFrom(this.automationStepService.getAutomationSteps()));
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
      await firstValueFrom(this.automationStepService.deleteAutomationStep(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}