import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { HomeService } from '../../core/api/Home.service';

@Component({
  selector: 'app-home',
  styleUrl: '../shared/list.css',
  templateUrl: './home.html',
})
export class HomeComponent implements OnInit {
  private readonly homeService = inject(HomeService);

  readonly status = signal('');
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    this.status.set('');
    try {
      await firstValueFrom(this.homeService.getHome());
      this.status.set('OK');
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}