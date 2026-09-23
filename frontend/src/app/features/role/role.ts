import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { RoleService, Role } from '../../core/api/Role.service';

@Component({
  selector: 'app-role',
  styleUrl: '../shared/list.css',
  templateUrl: './role.html',
})
export class RoleComponent implements OnInit {
  private readonly roleService = inject(RoleService);

  readonly roles = signal<Role[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.roles.set(await firstValueFrom(this.roleService.getRoles()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}