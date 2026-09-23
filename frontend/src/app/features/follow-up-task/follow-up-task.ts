import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FollowUpTask, FollowUpTaskService } from '../../core/api/FollowUpTask.service';

@Component({
  selector: 'app-follow-up-task',
  styleUrl: '../shared/list.css',
  templateUrl: './follow-up-task.html',
})
export class FollowUpTaskComponent implements OnInit {
  private readonly followUpTaskService = inject(FollowUpTaskService);

  readonly followUpTasks = signal<FollowUpTask[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);
  readonly completingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.followUpTasks.set(await firstValueFrom(this.followUpTaskService.getFollowUpTasks()));
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
      await firstValueFrom(this.followUpTaskService.deleteFollowUpTask(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }

  async markComplete(task: FollowUpTask): Promise<void> {
    this.completingId.set(task.id);
    this.error.set('');
    try {
      await firstValueFrom(this.followUpTaskService.markComplete(task.id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.completingId.set(null);
    }
  }
}