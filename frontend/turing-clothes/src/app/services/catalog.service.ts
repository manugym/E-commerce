import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ProductDto } from '../models/product-dto';
import { Result } from '../models/result';
import { CatalogoComponent } from '../pages/catalogo/catalogo.component';
import { PaginationParams } from '../models/pagination-params';
import { PagedResults } from '../models/paged-results';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  constructor(private api: ApiService) {}

  async getPagedResults(paginationParams: PaginationParams): Promise<Result<PagedResults>> {
    const result = await this.api.get<PagedResults>(`Catalog/ProductosPaginados?Query=${paginationParams.query}&PageNumber=${paginationParams.pageNumber}&OrderBy=${paginationParams.orderBy}&Direction=${paginationParams.direction}&PageSize=${paginationParams.pageSize}`, {
      params: {
        query: paginationParams.query,
        pageNumber: paginationParams.pageNumber.toString(),
        pageSize: paginationParams.pageSize.toString(),
        orderBy: paginationParams.orderBy.toString(),
        direction: paginationParams.direction.toString(),
      },
    });
  
    return result;
  }
}
