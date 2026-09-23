import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { LeadScoreDto, LeadScoringService } from '../../core/api/LeadScoring.service';

@Component({
  selector: 'app-lead-scoring',
  styleUrl: '../shared/list.css',
  templateUrl: './lead-scoring.html',
})
export class LeadScoringComponent implements OnInit {
  private readonly leadScoringService = inject(LeadScoringService);

  readonly query = signal('');
  readonly leadScores = signal<LeadScoreDto[]>([]);
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
      const q = this.query().trim() || undefined;
      const viewModel = await firstValueFrom(
        q ? this.leadScoringService.getLeadScoresByType(q) : this.leadScoringService.getLeadScores(),
      );
      this.leadScores.set(viewModel.leadScores);
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
      await firstValueFrom(this.leadScoringService.deleteLeadScore(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}