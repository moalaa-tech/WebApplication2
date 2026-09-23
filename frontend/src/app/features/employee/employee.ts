import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Employee, EmployeeService } from '../../core/api/Employee.service';

@Component({
  selector: 'app-employee',
  styleUrl: '../shared/list.css',
  templateUrl: './employee.html',
})
export class EmployeeComponent implements OnInit {
  private readonly employeeService = inject(EmployeeService);

  readonly employees = signal<Employee[]>([]);
  readonly organizationChart = signal<unknown[][]>([]);
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
      this.employees.set(await firstValueFrom(this.employeeService.getEmployees()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    }
    try {
      this.organizationChart.set(
        await firstValueFrom(this.employeeService.getOrganizationChart()),
      );
    } catch {
      this.organizationChart.set([]);
    }
    this.loading.set(false);
  }

  async remove(id: number): Promise<void> {
    this.removingId.set(id);
    this.error.set('');
    try {
      await firstValueFrom(this.employeeService.deleteEmployee(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}