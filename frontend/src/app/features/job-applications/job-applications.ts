import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  JobApplication,
  JobApplicationsService,
  UpdateJobApplication,
} from '../../core/api/JobApplications.service';

@Component({
  selector: 'app-job-applications',
  styleUrl: '../shared/list.css',
  templateUrl: './job-applications.html',
})
export class JobApplicationsComponent implements OnInit {
  private readonly jobApplicationsService = inject(JobApplicationsService);

  readonly applications = signal<JobApplication[]>([]);
  readonly statusDrafts = signal<Record<number, string>>({});
  readonly interviewDrafts = signal<Record<number, string>>({});
  readonly loading = signal(true);
  readonly error = signal('');
  readonly busyId = signal<number | null>(null);
  readonly downloadingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const apps = await firstValueFrom(this.jobApplicationsService.getJobApplications());
      this.applications.set(apps);
      const status: Record<number, string> = {};
      const interviews: Record<number, string> = {};
      for (const app of apps) {
        status[app.id] = app.status;
        interviews[app.id] = app.interviewDate ?? '';
      }
      this.statusDrafts.set(status);
      this.interviewDrafts.set(interviews);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  setStatusDraft(id: number, value: string): void {
    this.statusDrafts.update((drafts) => ({ ...drafts, [id]: value }));
  }

  setInterviewDraft(id: number, value: string): void {
    this.interviewDrafts.update((drafts) => ({ ...drafts, [id]: value }));
  }

  async updateStatus(id: number): Promise<void> {
    const app = this.applications().find((a) => a.id === id);
    if (!app) return;
    this.busyId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(
        this.jobApplicationsService.updateStatus(
          id,
          this.toUpdateDto(app, this.statusDrafts()[id] ?? app.status, this.interviewDrafts()[id] ?? ''),
        ),
      );
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busyId.set(null);
    }
  }

  async scheduleInterview(id: number): Promise<void> {
    const app = this.applications().find((a) => a.id === id);
    if (!app) return;
    this.busyId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(
        this.jobApplicationsService.scheduleInterview(
          id,
          this.toUpdateDto(app, this.statusDrafts()[id] ?? app.status, this.interviewDrafts()[id] ?? ''),
        ),
      );
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busyId.set(null);
    }
  }

  async downloadResume(id: number): Promise<void> {
    const app = this.applications().find((a) => a.id === id);
    if (!app) return;
    await this.download(id, async () => {
      const blob = await firstValueFrom(this.jobApplicationsService.downloadResume(id));
      this.saveBlob(blob, `${app.fullName}-resume`);
    });
  }

  async downloadCoverLetter(id: number): Promise<void> {
    const app = this.applications().find((a) => a.id === id);
    if (!app) return;
    await this.download(id, async () => {
      const blob = await firstValueFrom(this.jobApplicationsService.downloadCoverLetter(id));
      this.saveBlob(blob, `${app.fullName}-cover-letter`);
    });
  }

  private async download(id: number, action: () => Promise<void>): Promise<void> {
    this.downloadingId.set(id);
    this.error.set('');
    try {
      await action();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.downloadingId.set(null);
    }
  }

  private saveBlob(blob: Blob, filename: string): void {
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;
    anchor.click();
    URL.revokeObjectURL(url);
  }

  private toUpdateDto(
    app: JobApplication,
    status: string,
    interviewDate: string,
  ): UpdateJobApplication {
    return {
      id: app.id,
      firstName: app.firstName,
      lastName: app.lastName,
      email: app.email,
      phone: app.phone,
      resumePath: app.resumePath,
      coverLetterPath: app.coverLetterPath,
      jobPostingId: app.jobPostingId,
      jobTitle: app.jobTitle,
      status,
      applicationDate: app.applicationDate,
      interviewDate: interviewDate || null,
      notes: app.notes,
    };
  }
}