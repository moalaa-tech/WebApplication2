import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientBase } from './api-client-base';

export interface Quote {
  id: number;
  quoteNumber: string;
  issueDate: string;
  expiryDate: string;
  termsAndConditions: string;
  notes: string;
  subTotal: number;
  taxAmount: number;
  discountAmount: number;
  totalAmount: number;
  products?: Product[];
  lineItems?: QuoteLineItem[];
  deals?: Deal[];
}

export interface Product {
  id?: number | null;
  name?: string | null;
  nameAr?: string | null;
  totalCost?: number | null;
  shippingCost?: number | null;
  freightCost?: number | null;
  salesPrice?: number | null;
  productTypeId?: number | null;
  productTypeName?: string | null;
  imageFile?: string | null;
  description?: string | null;
  currentStock?: number | null;
  minimumStockLevel?: number | null;
  sku?: string | null;
}

export interface QuoteLineItem {
  id: number;
  quoteId: number;
  productId: number;
  productName: string;
  productDescription?: string;
  productCode?: string;
  quantity: number;
  unitPrice: number;
  discountPercentage: number;
  description: string;
  lineTotal: number;
}

export interface Deal {
  id: number;
  status?: string;
  opportunityId: number;
  opportunityName: string;
  finalValue: number;
  description: string;
  dealOwner: string;
  name: string;
  accountName: string;
  type: boolean;
  amount: number;
  closingDate: string;
  contactId: number;
  contactName: string;
  files?: DealFile[];
  expectedCloseDate?: string;
}

export interface DealFile {
  id: number;
  fileName: string;
  filePath: string;
}

export interface QuoteFilter {
  searchTerm?: string | null;
  fromDate?: string | null;
  toDate?: string | null;
}

export interface PagingInfo {
  totalItems: number;
  itemsPerPage: number;
  currentPage: number;
  totalPages: number;
}

export interface QuoteListPage {
  quotes: Quote[];
  pagingInfo: PagingInfo;
  filter: QuoteFilter;
}

export interface SelectListItem {
  value: string;
  text: string;
  selected: boolean;
  disabled: boolean;
}

export interface AssociateDeal {
  quoteId: number;
  quoteNumber: string;
  dealId: number;
  deals?: SelectListItem[];
}

@Injectable({ providedIn: 'root' })
export class QuotesService extends ApiClientBase {
  protected readonly endpoint = 'Quotes';

  constructor(http: HttpClient) {
    super(http);
  }

  getQuotes(page?: number, searchTerm?: string, fromDate?: string, toDate?: string): Observable<QuoteListPage> {
    return this.http.get<QuoteListPage>(this.url(), {
      params: this.params({ page, searchTerm, fromDate, toDate }),
    });
  }

  getQuote(id: number): Observable<Quote> {
    return this.http.get<Quote>(this.url(`/${id}`));
  }

  createQuote(quote: Quote): Observable<Quote> {
    return this.http.post<Quote>(this.url(), quote);
  }

  updateQuote(id: number, quote: Quote): Observable<Quote> {
    return this.http.put<Quote>(this.url(`/${id}`), quote);
  }

  deleteQuote(id: number): Observable<void> {
    return this.http.delete<void>(this.url(`/${id}`));
  }

  addLineItem(lineItem: QuoteLineItem): Observable<QuoteLineItem> {
    return this.http.post<QuoteLineItem>(this.url('/AddLineItem'), lineItem);
  }

  updateLineItem(lineItem: QuoteLineItem): Observable<QuoteLineItem> {
    return this.http.post<QuoteLineItem>(this.url('/UpdateLineItem'), lineItem);
  }

  removeLineItem(id: number, quoteId: number): Observable<void> {
    return this.http.post<void>(this.url('/RemoveLineItem'), null, {
      params: this.params({ id, quoteId }),
    });
  }

  getAssociateDeal(id: number): Observable<AssociateDeal> {
    return this.http.get<AssociateDeal>(this.url('/AssociateDeal'), {
      params: this.params({ id }),
    });
  }

  associateDeal(model: AssociateDeal): Observable<AssociateDeal> {
    return this.http.post<AssociateDeal>(this.url('/AssociateDeal'), model);
  }
}