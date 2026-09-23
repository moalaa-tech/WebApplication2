import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { JobTitle, JobTitleService } from '../../core/api/JobTitle.service';

@Component({
  selector: 'app-job-title',
  styleUrl: '../shared/list.css',
  templateUrl: './job-title.html',
})
export class JobTitleComponent implements OnInit {
  private readonly jobTitleService = inject(JobTitleService);

  readonly jobTitles = signal<JobTitle[]>([]);
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
      this.jobTitles.set(await firstValueFrom(this.jobTitleService.getJobTitles()));
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
      await firstValueFrom(this.jobTitleService.deleteJobTitle(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}