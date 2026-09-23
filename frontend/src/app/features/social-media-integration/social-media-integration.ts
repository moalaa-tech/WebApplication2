import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { SocialMediaIntegrationService, SocialMediaPostDto } from '../../core/api/SocialMediaIntegration.service';

@Component({
  selector: 'app-social-media-integration',
  styleUrl: '../shared/list.css',
  templateUrl: './social-media-integration.html',
})
export class SocialMediaIntegrationComponent implements OnInit {
  private readonly socialMediaIntegrationService = inject(SocialMediaIntegrationService);

  readonly posts = signal<SocialMediaPostDto[]>([]);
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
      this.posts.set(await firstValueFrom(this.socialMediaIntegrationService.getSocialMediaPosts()));
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
      await firstValueFrom(this.socialMediaIntegrationService.deleteSocialMediaPost(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}