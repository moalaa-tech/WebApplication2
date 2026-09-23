import { Component, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { InstagramWebhookService } from '../../core/api/InstagramWebhook.service';

@Component({
  selector: 'app-instagram-webhook',
  styleUrl: '../shared/list.css',
  templateUrl: './instagram-webhook.html',
})
export class InstagramWebhookComponent {
  private readonly webhookService = inject(InstagramWebhookService);

  readonly mode = signal('subscribe');
  readonly verifyToken = signal('');
  readonly challenge = signal('');
  readonly result = signal('');
  readonly loading = signal(false);
  readonly error = signal('');

  verifyUrl(): string {
    const params = new URLSearchParams({
      'hub.mode': this.mode(),
      'hub.verify_token': this.verifyToken(),
      'hub.challenge': this.challenge(),
    });
    return `/api/InstagramWebhook?${params.toString()}`;
  }

  async verify(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.result.set(
        await firstValueFrom(this.webhookService.verifyWebhook(this.mode(), this.challenge(), this.verifyToken())),
      );
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}