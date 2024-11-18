import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ProductDto } from '../models/product-dto';
import { Result } from '../models/result';
import { PaginationParams } from '../models/pagination-params';
import { PagedResults } from '../models/paged-results';
import { ReviewDto } from '../models/review-dto';
import { CartServiceService } from './cart-service.service';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  private storageKey = 'catalogSettings';

  constructor(private api: ApiService) {}

  async getPagedResults(
    paginationParams: PaginationParams
  ): Promise<Result<PagedResults>> {
    // Guardar la configuración actual en sessionStorage
    this.saveUserSettings(paginationParams);

    const result = await this.api.get<PagedResults>(
      `Catalog/ProductosPaginados?Query=${paginationParams.query}&PageNumber=${paginationParams.pageNumber}&OrderBy=${paginationParams.orderBy}&Direction=${paginationParams.direction}&PageSize=${paginationParams.pageSize}`,
      {
        params: {
          query: paginationParams.query,
          pageNumber: paginationParams.pageNumber,
          pageSize: paginationParams.pageSize,
          orderBy: paginationParams.orderBy,
          direction: paginationParams.direction,
        },
      }
    );

    return result;
  }

  async getProductDetailsById(id: number): Promise<Result<ProductDto>> {
    const result = await this.api.get<ProductDto>(
      `Catalog/GetProduct?id=${id}`
    );

    result.data.image = `https://localhost:7183/${result.data.image}`;

    return result;
  }

  saveUserSettings(paginationParams: PaginationParams): void {
    sessionStorage.setItem(this.storageKey, JSON.stringify(paginationParams));
  }

  getUserSettings(): PaginationParams {
    const settings = sessionStorage.getItem(this.storageKey);

    if (settings) {
      try {
        const parsedSettings = JSON.parse(settings);
        return parsedSettings;
      } catch (e) {
        console.error('Error al parsear los ajustes del usuario:', e);
      }
    }

    // Si no hay datos o si ocurre un error, devolvemos los valores predeterminados
    return {
      query: '',
      pageNumber: 1,
      pageSize: 8,
      orderBy: 2,
      direction: 0,
    };
  }

  async addReview(review: ReviewDto): Promise<Result<ReviewDto>> {
    return await this.api.post<ReviewDto>('Review', review);
  }
}
