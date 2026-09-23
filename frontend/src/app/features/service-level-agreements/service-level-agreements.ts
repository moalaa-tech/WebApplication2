import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ServiceLevelAgreementsService, ServiceLevelAgreement } from '../../core/api/ServiceLevelAgreements.service';

@Component({
  selector: 'app-service-level-agreements',
  styleUrl: '../shared/list.css',
  templateUrl: './service-level-agreements.html',
})
export class ServiceLevelAgreementsComponent implements OnInit {
  private readonly serviceLevelAgreementsService = inject(ServiceLevelAgreementsService);

  readonly agreements = signal<ServiceLevelAgreement[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.agreements.set(await firstValueFrom(this.serviceLevelAgreementsService.getServiceLevelAgreements()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}