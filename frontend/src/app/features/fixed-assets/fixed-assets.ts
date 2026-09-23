import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FixedAsset, FixedAssetsService } from '../../core/api/FixedAssets.service';

@Component({
  selector: 'app-fixed-assets',
  styleUrl: '../shared/list.css',
  templateUrl: './fixed-assets.html',
})
export class FixedAssetsComponent implements OnInit {
  private readonly fixedAssetsService = inject(FixedAssetsService);

  readonly assets = signal<FixedAsset[]>([]);
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
      this.assets.set(await firstValueFrom(this.fixedAssetsService.getFixedAssets()));
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
      await firstValueFrom(this.fixedAssetsService.deleteFixedAsset(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}