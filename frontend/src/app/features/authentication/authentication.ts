import { Component, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AuthenticationService } from '../../core/api/Authentication.service';

@Component({
  selector: 'app-authentication',
  styleUrl: '../shared/list.css',
  templateUrl: './authentication.html',
})
export class AuthenticationComponent {
  private readonly authenticationService = inject(AuthenticationService);

  readonly email = signal('');
  readonly password = signal('');
  readonly result = signal('');
  readonly error = signal('');
  readonly signedOut = signal(true);
  readonly busy = signal(false);

  async login(): Promise<void> {
    this.busy.set(true);
    this.error.set('');
    this.result.set('');
    try {
      const outcome = await firstValueFrom(
        this.authenticationService.login({
          email: this.email(),
          password: this.password(),
          rememberMe: false,
        }),
      );
      if (outcome.succeeded) {
        this.result.set(outcome.message ?? 'Login succeeded.');
        this.signedOut.set(false);
      } else {
        this.error.set(this.describe(outcome.errors, outcome.message, 'Login failed.'));
      }
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busy.set(false);
    }
  }

  async register(): Promise<void> {
    this.busy.set(true);
    this.error.set('');
    this.result.set('');
    try {
      const localPart = this.email().split('@')[0] ?? '';
      const outcome = await firstValueFrom(
        this.authenticationService.register({
          email: this.email(),
          password: this.password(),
          confirmPassword: this.password(),
          firstName: localPart,
          lastName: '',
        }),
      );
      if (outcome.succeeded) {
        this.result.set(outcome.message ?? 'Registration succeeded.');
      } else {
        this.error.set(this.describe(outcome.errors, outcome.message, 'Registration failed.'));
      }
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busy.set(false);
    }
  }

  async logout(): Promise<void> {
    this.busy.set(true);
    this.error.set('');
    this.result.set('');
    try {
      const outcome = await firstValueFrom(this.authenticationService.logout());
      if (outcome.succeeded) {
        this.result.set(outcome.message ?? 'Logged out.');
        this.signedOut.set(true);
      } else {
        this.error.set(this.describe(outcome.errors, outcome.message, 'Logout failed.'));
      }
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.busy.set(false);
    }
  }

  private describe(errors: string[] | undefined, message: string | undefined, fallback: string): string {
    if (errors && errors.length) {
      return errors.join(' ');
    }
    return message ?? fallback;
  }
}
