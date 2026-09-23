import { Component, inject, OnInit, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ProductDto, ProductService } from '../../core/api/Product.service';

@Component({
  selector: 'app-product',
  styleUrl: '../shared/list.css',
  templateUrl: './product.html',
})
export class ProductComponent implements OnInit {
  private readonly productService = inject(ProductService);

  readonly products = signal<ProductDto[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  async ngOnInit(): Promise<void> {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      this.products.set(await firstValueFrom(this.productService.getProducts()));
    } catch (e) {
      this.error.set(e instanceof Error ? e.message : String(e));
    } finally {
      this.loading.set(false);
    }
  }
}