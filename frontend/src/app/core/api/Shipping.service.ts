import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface ShippingCarrier {
  id: number;
  name: string;
  nameAr: string;
  isActive: boolean;
  supportedServices?: string[];
  supportedCountries?: string[];
  maximumPackageWeight: number;
  packageTypes?: string[];
  description?: string;
  dateModified?: string | null;
  dateCreated?: string | null;
}

export interface SelectListItem {
  value: string;
  text: string;
  selected: boolean;
  disabled: boolean;
}

export interface Package {
  weight: number;
  length?: number;
  width?: number;
  height?: number;
}

export interface ShippingRequest {
  originStreet: string;
  originCity: string;
  originState: string;
  originZipCode: string;
  originCountry: string;
  destinationStreet: string;
  destinationCity: string;
  destinationState: string;
  destinationZipCode: string;
  destinationCountry: string;
  packages?: Package[];
  carrierId?: string;
  availableCarriers?: SelectListItem[];
}

export interface ShippingQuote {
  quoteId?: string;
  totalCost: number;
  estimatedDeliveryDate: string;
  carrierServiceName?: string;
  trackingNumber?: string;
  quoteExpiration?: string;
}

export interface ShippingOption {
  carrierId: string;
  carrierName: string;
  serviceId: string;
  serviceName: string;
  cost: number;
  currency: string;
  estimatedDeliveryDate: string;
  transitDays: number;
  requiresSignature: boolean;
  isInsured: boolean;
  maximumInsuranceValue: number;
  packageTypes?: string[];
  maximumWeight: number;
  weightUnit: string;
}

export interface TrackShipment {
  trackingNumber: string;
}

export interface TrackingEvent {
  timestamp: string;
  location?: string;
  description?: string;
  status?: string;
  eventCode?: string;
}

export interface AddressInfo {
  name?: string;
  company?: string;
  street1?: string;
  street2?: string;
  city?: string;
  state?: string;
  postalCode?: string;
  country?: string;
  phone?: string;
  email?: string;
}

export interface ShipmentTrackingInfo {
  trackingNumber: string;
  carrier?: string;
  status?: string;
  currentStatus?: string;
  estimatedDeliveryDate?: string | null;
  actualDeliveryDate?: string | null;
  events?: TrackingEvent[];
  origin?: AddressInfo;
  destination?: AddressInfo;
  weight: number;
  weightUnit?: string;
}

export interface ShippingLabel {
  labelData?: string;
  fileFormat?: string;
  trackingNumber?: string;
  labelUrl?: string;
}

export interface OperationResult {
  success: boolean;
  message: string;
}

@Injectable({ providedIn: 'root' })
export class ShippingService extends ApiClientBase {
  protected readonly endpoint = 'Shipping';

  constructor(http: HttpClient) {
    super(http);
  }

  getShipping(): Observable<ShippingCarrier[]> {
    return this.http.get<ShippingCarrier[]>(this.url());
  }

  getCalculate(): Observable<ShippingRequest> {
    return this.http.get<ShippingRequest>(this.url('/Calculate'));
  }

  calculateShipping(model: ShippingRequest): Observable<ShippingQuote> {
    return this.http.post<ShippingQuote>(this.url('/Calculate'), model);
  }

  getQuote(): Observable<ShippingRequest> {
    return this.http.get<ShippingRequest>(this.url('/Quote'));
  }

  getShippingQuote(model: ShippingRequest): Observable<ShippingQuote> {
    return this.http.post<ShippingQuote>(this.url('/Quote'), model);
  }

  getOptions(): Observable<ShippingRequest> {
    return this.http.get<ShippingRequest>(this.url('/Options'));
  }

  getShippingOptions(model: ShippingRequest): Observable<ShippingOption[]> {
    return this.http.post<ShippingOption[]>(this.url('/Options'), model);
  }

  getTrack(): Observable<TrackShipment> {
    return this.http.get<TrackShipment>(this.url('/Track'));
  }

  trackShipment(model: TrackShipment): Observable<ShipmentTrackingInfo> {
    return this.http.post<ShipmentTrackingInfo>(this.url('/Track'), model);
  }

  getCreateLabel(): Observable<ShippingRequest> {
    return this.http.get<ShippingRequest>(this.url('/CreateLabel'));
  }

  createShippingLabel(model: ShippingRequest): Observable<ShippingLabel> {
    return this.http.post<ShippingLabel>(this.url('/CreateLabel'), model);
  }

  cancelShipment(trackingNumber: string): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.url('/Cancel'), null, {
      params: this.params({ trackingNumber }),
    });
  }
}