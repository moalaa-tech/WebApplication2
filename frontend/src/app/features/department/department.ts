import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Department, DepartmentService } from '../../core/api/Department.service';

@Component({
  selector: 'app-department',
  styleUrl: '../shared/list.css',
  templateUrl: './department.html',
})
export class DepartmentComponent implements OnInit {
  private readonly departmentService = inject(DepartmentService);

  readonly departments = signal<Department[]>([]);
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
      this.departments.set(await firstValueFrom(this.departmentService.getDepartments()));
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
      await firstValueFrom(this.departmentService.deleteDepartment(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}