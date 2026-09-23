import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CampaignDto, CampaignStatus, CampaignsService } from '../../core/api/Campaigns.service';

@Component({
  selector: 'app-campaigns',
  styleUrl: '../shared/list.css',
  templateUrl: './campaigns.html',
})
export class CampaignsComponent implements OnInit {
  private readonly campaignsService = inject(CampaignsService);

  readonly campaigns = signal<CampaignDto[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly changingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.campaigns.set(await firstValueFrom(this.campaignsService.getCampaigns()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async toggleStatus(campaign: CampaignDto): Promise<void> {
    const status: CampaignStatus = campaign.status === 'Active' ? 'Paused' : 'Active';
    this.changingId.set(campaign.id);
    this.error.set('');
    try {
      await firstValueFrom(this.campaignsService.changeStatus(campaign.id, status));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.changingId.set(null);
    }
  }
}