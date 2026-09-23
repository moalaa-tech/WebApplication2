import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { JobPosting, JobPostingsService } from '../../core/api/JobPostings.service';

@Component({
  selector: 'app-job-postings',
  styleUrl: '../shared/list.css',
  templateUrl: './job-postings.html',
})
export class JobPostingsComponent implements OnInit {
  private readonly jobPostingsService = inject(JobPostingsService);

  readonly activeOnly = signal(true);
  readonly postings = signal<JobPosting[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);
  readonly closingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.postings.set(
        await firstValueFrom(this.jobPostingsService.getJobPostings(this.activeOnly())),
      );
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
      await firstValueFrom(this.jobPostingsService.deleteJobPosting(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }

  async close(id: number): Promise<void> {
    this.closingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.jobPostingsService.closeJobPosting(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.closingId.set(null);
    }
  }
}