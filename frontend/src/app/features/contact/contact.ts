import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ContactService, ContactViewModel } from '../../core/api/Contact.service';

@Component({
  selector: 'app-contact',
  styleUrl: '../shared/list.css',
  templateUrl: './contact.html',
})
export class ContactComponent implements OnInit {
  private readonly contactService = inject(ContactService);

  readonly contacts = signal<ContactViewModel[]>([]);
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
      this.contacts.set(await firstValueFrom(this.contactService.getContacts()));
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
      await firstValueFrom(this.contactService.deleteContact(id));
      await this.load();
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.removingId.set(null);
    }
  }
}