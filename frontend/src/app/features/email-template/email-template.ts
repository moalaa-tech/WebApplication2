import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { EmailTemplate, EmailTemplateService } from '../../core/api/EmailTemplate.service';

@Component({
  selector: 'app-email-template',
  styleUrl: '../shared/list.css',
  templateUrl: './email-template.html',
})
export class EmailTemplateComponent implements OnInit {
  private readonly emailTemplateService = inject(EmailTemplateService);

  readonly templates = signal<EmailTemplate[]>([]);
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
      this.templates.set(await firstValueFrom(this.emailTemplateService.getEmailTemplates()));
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
      await firstValueFrom(this.emailTemplateService.deleteEmailTemplate(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}