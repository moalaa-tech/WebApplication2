import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CompanyService, CompanyViewModel } from '../../core/api/Company.service';

@Component({
  selector: 'app-company',
  styleUrl: '../shared/list.css',
  templateUrl: './company.html',
})
export class CompanyComponent implements OnInit {
  private readonly companyService = inject(CompanyService);

  readonly companies = signal<CompanyViewModel[]>([]);
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
      this.companies.set(await firstValueFrom(this.companyService.getCompanies()));
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
      await firstValueFrom(this.companyService.deleteCompany(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}
