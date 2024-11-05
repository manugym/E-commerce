import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { PagedResults } from '../models/paged-results';
import { PaginationParams } from '../models/pagination-params';
import { SmartSearchComponent } from '../pages/smart-search/smart-search.component';

@Injectable({
  providedIn: 'root'
})
export class SmartSearchService {
  paginationParams: PaginationParams;
  

  constructor(private api: ApiService) { }
  

  // async search(query: string): Promise<Result<string[]>> {
  //   return this.api.get<string[]>(`smartSearch?query=${query}`);
  // }

  async getPagedResults(paginationParams: PaginationParams): Promise<Result<PagedResults>> {
    
    
    const result = await this.api.get<PagedResults>('Catalog/ProductosPaginados', paginationParams);

    return result;
  }
}
