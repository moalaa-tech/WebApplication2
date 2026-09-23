import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Document, DocumentsService } from '../../core/api/Documents.service';

@Component({
  selector: 'app-documents',
  styleUrl: '../shared/list.css',
  templateUrl: './documents.html',
})
export class DocumentsComponent implements OnInit {
  private readonly documentsService = inject(DocumentsService);

  readonly query = signal('');
  readonly documents = signal<Document[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly removingId = signal<number | null>(null);
  readonly downloadingId = signal<number | null>(null);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const q = this.query().trim() || undefined;
      this.documents.set(await firstValueFrom(this.documentsService.getDocuments(q)));
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
      await firstValueFrom(this.documentsService.deleteDocument(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }

  async download(id: number): Promise<void> {
    this.downloadingId.set(id);
    this.error.set('');
    try {
      const blob = await firstValueFrom(this.documentsService.downloadDocument(id));
      const url = URL.createObjectURL(blob);
      const anchor = document.createElement('a');
      anchor.href = url;
      anchor.download = this.documents().find((d) => d.id === id)?.fileName ?? 'download';
      anchor.click();
      URL.revokeObjectURL(url);
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.downloadingId.set(null);
    }
  }
}