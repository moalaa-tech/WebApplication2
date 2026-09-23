import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CompaniesService, Company } from '../../core/api/Companies.service';

@Component({
  selector: 'app-companies',
  styleUrl: '../shared/list.css',
  templateUrl: './companies.html',
})
export class CompaniesComponent implements OnInit {
  private readonly companiesService = inject(CompaniesService);

  readonly companies = signal<Company[]>([]);
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
      this.companies.set(await firstValueFrom(this.companiesService.getCompanies()));
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
      await firstValueFrom(this.companiesService.deleteCompany(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}
