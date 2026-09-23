import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { DocumentFolder, DocumentFoldersService } from '../../core/api/DocumentFolders.service';

@Component({
  selector: 'app-document-folders',
  styleUrl: '../shared/list.css',
  templateUrl: './document-folders.html',
})
export class DocumentFoldersComponent implements OnInit {
  private readonly documentFoldersService = inject(DocumentFoldersService);

  readonly folders = signal<DocumentFolder[]>([]);
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
      this.folders.set(await firstValueFrom(this.documentFoldersService.getDocumentFolders()));
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
      await firstValueFrom(this.documentFoldersService.deleteDocumentFolder(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}