import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { UserService, User } from '../../core/api/User.service';

@Component({
  selector: 'app-user',
  styleUrl: '../shared/list.css',
  templateUrl: './user.html',
})
export class UserComponent implements OnInit {
  private readonly userService = inject(UserService);

  readonly users = signal<User[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.users.set(await firstValueFrom(this.userService.getUsers()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}