import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { DocumentCategoriesService, DocumentCategory } from '../../core/api/DocumentCategories.service';

@Component({
  selector: 'app-document-categories',
  styleUrl: '../shared/list.css',
  templateUrl: './document-categories.html',
})
export class DocumentCategoriesComponent implements OnInit {
  private readonly documentCategoriesService = inject(DocumentCategoriesService);

  readonly categories = signal<DocumentCategory[]>([]);
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
      this.categories.set(await firstValueFrom(this.documentCategoriesService.getDocumentCategories()));
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
      await firstValueFrom(this.documentCategoriesService.deleteDocumentCategory(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}