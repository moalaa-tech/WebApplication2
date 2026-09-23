import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Segment, SegmentationService } from '../../core/api/Segmentation.service';

@Component({
  selector: 'app-segmentation',
  styleUrl: '../shared/list.css',
  templateUrl: './segmentation.html',
})
export class SegmentationComponent implements OnInit {
  private readonly segmentationService = inject(SegmentationService);

  readonly segments = signal<Segment[]>([]);
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
      this.segments.set(await firstValueFrom(this.segmentationService.getSegments()));
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
      await firstValueFrom(this.segmentationService.deleteSegment(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}