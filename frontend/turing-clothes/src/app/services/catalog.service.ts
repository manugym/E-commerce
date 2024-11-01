import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ProductDto } from '../models/product-dto';
import { Result } from '../models/result';
import { CatalogoComponent } from '../pages/catalogo/catalogo.component';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  constructor(private api: ApiService) {}

  async getAllProducts(): Promise<Result<ProductDto[]>> {
    const result = await this.api.get<ProductDto[]>('Catalog/ObtenerProductos');

    return result;
  }
}
