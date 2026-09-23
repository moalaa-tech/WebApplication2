import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ExpenseCategoriesService, ExpenseCategory } from '../../core/api/ExpenseCategories.service';

@Component({
  selector: 'app-expense-categories',
  styleUrl: '../shared/list.css',
  templateUrl: './expense-categories.html',
})
export class ExpenseCategoriesComponent implements OnInit {
  private readonly expenseCategoriesService = inject(ExpenseCategoriesService);

  readonly categories = signal<ExpenseCategory[]>([]);
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
      this.categories.set(await firstValueFrom(this.expenseCategoriesService.getExpenseCategories()));
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
      await firstValueFrom(this.expenseCategoriesService.deleteExpenseCategory(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}