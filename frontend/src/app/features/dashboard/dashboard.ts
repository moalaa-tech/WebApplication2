import {
  Component,
  computed,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { HealthLive, HealthService } from '../../core/api/Health.service';
import { Activity, ActivitiesService } from '../../core/api/Activities.service';
import { Company, CompaniesService } from '../../core/api/Companies.service';
import { Vendor, VendorsService } from '../../core/api/Vendors.service';

@Component({
  selector: 'app-dashboard',
  styleUrl: './dashboard.css',
  templateUrl: './dashboard.html',
})
export class Dashboard implements OnInit {
  private readonly healthService = inject(HealthService);
  private readonly vendorsService = inject(VendorsService);
  private readonly companiesService = inject(CompaniesService);
  private readonly activitiesService = inject(ActivitiesService);

  readonly health = signal<HealthLive | null>(null);
  readonly vendors = signal<Vendor[]>([]);
  readonly companies = signal<Company[]>([]);
  readonly activities = signal<Activity[]>([]);
  readonly activityCount = signal(0);

  readonly loading = signal(true);
  readonly error = signal('');

  readonly apiHealthy = computed(() => this.health()?.status?.toLowerCase() === 'healthy');

  async ngOnInit(): Promise<void> {
    try {
      this.health.set(await firstValueFrom(this.healthService.getLive()));

      const [vendors, activityPage, companies] = await Promise.all([
        firstValueFrom(this.vendorsService.getVendors()),
        firstValueFrom(this.activitiesService.getActivities(1)),
        firstValueFrom(this.companiesService.getCompanies()),
      ]);

      this.vendors.set(vendors);
      this.activities.set(activityPage.activities.slice(0, 5));
      this.activityCount.set(activityPage.activities.totalCount);
      this.companies.set(companies);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}