import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { LeaveType, LeaveTypesService } from '../../core/api/LeaveTypes.service';

@Component({
  selector: 'app-leave-types',
  styleUrl: '../shared/list.css',
  templateUrl: './leave-types.html',
})
export class LeaveTypesComponent implements OnInit {
  private readonly leaveTypesService = inject(LeaveTypesService);

  readonly leaveTypes = signal<LeaveType[]>([]);
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
      this.leaveTypes.set(await firstValueFrom(this.leaveTypesService.getLeaveTypes()));
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
      await firstValueFrom(this.leaveTypesService.deleteLeaveType(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}