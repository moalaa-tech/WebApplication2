import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { NotificationsService, Notification } from '../../core/api/Notifications.service';

@Component({
  selector: 'app-notifications',
  styleUrl: '../shared/list.css',
  templateUrl: './notifications.html',
})
export class NotificationsComponent implements OnInit {
  private readonly notificationsService = inject(NotificationsService);

  readonly notifications = signal<Notification[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly busyId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.notifications.set(await firstValueFrom(this.notificationsService.getNotifications()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async markAsRead(id: number): Promise<void> {
    this.busyId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.notificationsService.markAsRead(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busyId.set(null);
    }
  }

  async remove(id: number): Promise<void> {
    this.busyId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.notificationsService.deleteNotification(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busyId.set(null);
    }
  }
}