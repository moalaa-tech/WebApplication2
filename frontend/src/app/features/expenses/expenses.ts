import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Expense, ExpensesService } from '../../core/api/Expenses.service';

@Component({
  selector: 'app-expenses',
  styleUrl: '../shared/list.css',
  templateUrl: './expenses.html',
})
export class ExpensesComponent implements OnInit {
  private readonly expensesService = inject(ExpensesService);

  readonly query = signal('');
  readonly expenses = signal<Expense[]>([]);
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
      const q = this.query().trim() || undefined;
      const result = await firstValueFrom(this.expensesService.getExpenses({ search: q }));
      this.expenses.set(result.items);
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
      await firstValueFrom(this.expensesService.deleteExpense(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}