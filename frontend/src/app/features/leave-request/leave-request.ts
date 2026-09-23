import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { LeaveRequest, LeaveRequestService } from '../../core/api/LeaveRequest.service';

@Component({
  selector: 'app-leave-request',
  styleUrl: '../shared/list.css',
  templateUrl: './leave-request.html',
})
export class LeaveRequestComponent implements OnInit {
  private readonly leaveRequestService = inject(LeaveRequestService);

  readonly leaveRequests = signal<LeaveRequest[]>([]);
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
      this.leaveRequests.set(
        await firstValueFrom(this.leaveRequestService.getLeaveRequests()),
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
      await firstValueFrom(this.leaveRequestService.deleteLeaveRequest(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}