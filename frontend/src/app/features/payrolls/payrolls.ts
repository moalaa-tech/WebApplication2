import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { PayrollDto, PayrollsService } from '../../core/api/Payrolls.service';

@Component({
  selector: 'app-payrolls',
  styleUrl: '../shared/list.css',
  templateUrl: './payrolls.html',
})
export class PayrollsComponent implements OnInit {
  private readonly payrollsService = inject(PayrollsService);

  readonly startDate = signal(
    this.toDateInput(new Date(Date.now() - 30 * 86400000)),
  );
  readonly endDate = signal(this.toDateInput(new Date()));
  readonly payrolls = signal<PayrollDto[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  onStart(date: string): void {
    if (!date || date === this.startDate()) return;
    this.startDate.set(date);
    void this.load();
  }

  onEnd(date: string): void {
    if (!date || date === this.endDate()) return;
    this.endDate.set(date);
    void this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.payrolls.set(
        await firstValueFrom(
          this.payrollsService.getPayrolls(this.startDate(), this.endDate()),
        ),
      );
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  private toDateInput(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
}