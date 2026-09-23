import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ShipmentTrackingInfo, ShippingCarrier, ShippingService } from '../../core/api/Shipping.service';

@Component({
  selector: 'app-shipping',
  styleUrl: '../shared/list.css',
  templateUrl: './shipping.html',
})
export class ShippingComponent implements OnInit {
  private readonly shippingService = inject(ShippingService);

  readonly carriers = signal<ShippingCarrier[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');
  readonly trackingNumber = signal('');
  readonly tracking = signal<ShipmentTrackingInfo | null>(null);
  readonly trackingLoading = signal(false);

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.carriers.set(await firstValueFrom(this.shippingService.getShipping()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }

  async track(): Promise<void> {
    const trackingNumber = this.trackingNumber().trim();
    if (!trackingNumber) {
      return;
    }
    this.trackingLoading.set(true);
    this.error.set('');
    this.tracking.set(null);
    try {
      this.tracking.set(await firstValueFrom(this.shippingService.trackShipment({ trackingNumber })));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.trackingLoading.set(false);
    }
  }
}
