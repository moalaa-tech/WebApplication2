import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { SettingService, Setting } from '../../core/api/Setting.service';

@Component({
  selector: 'app-setting',
  styleUrl: '../shared/list.css',
  templateUrl: './setting.html',
})
export class SettingComponent implements OnInit {
  private readonly settingService = inject(SettingService);

  readonly settings = signal<Setting[]>([]);
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
      this.settings.set(await firstValueFrom(this.settingService.getSettings()));
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
      await firstValueFrom(this.settingService.deleteSetting(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}