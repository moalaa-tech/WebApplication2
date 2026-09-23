import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { JobPhase, JobPhasesService } from '../../core/api/JobPhases.service';

@Component({
  selector: 'app-job-phases',
  styleUrl: '../shared/list.css',
  templateUrl: './job-phases.html',
})
export class JobPhasesComponent implements OnInit {
  private readonly jobPhasesService = inject(JobPhasesService);

  readonly projectId = signal(1);
  readonly jobPhases = signal<JobPhase[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  onProjectIdChange(value: string): void {
    const parsed = Number(value);
    if (!Number.isInteger(parsed) || parsed <= 0) {
      return;
    }
    this.projectId.set(parsed);
    void this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const id = this.projectId();
      if (!Number.isInteger(id) || id <= 0) {
        this.jobPhases.set([]);
        return;
      }
      this.jobPhases.set(await firstValueFrom(this.jobPhasesService.getJobPhases(id)));
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
      await firstValueFrom(this.jobPhasesService.deleteJobPhase(this.projectId(), id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}