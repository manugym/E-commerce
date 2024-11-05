import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SmartSearchService } from '../../services/smart-search.service';
import { PagedResults } from '../../models/paged-results';
import { PaginationParams } from '../../models/pagination-params';
import { ProductDto } from '../../models/product-dto';

@Component({
  selector: 'app-smart-search',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './smart-search.component.html',
  styleUrl: './smart-search.component.css',
})
export class SmartSearchComponent implements OnInit {
  query: string = '';
  items: ProductDto[] = [];

  /**
   * Esto está puesto provisional. REQUIERE REVISIÓN DEL LÍDER.
   */
  paginationParams: PaginationParams = {
    query: '',
    pageNumber: 1,
    orderBy: 0, // Default: 0 (por ejemplo, precio)
    direction: 0, // Default: 0 (ascendente)
  };
  isAscending: boolean;

  pagedResults: PagedResults;

  constructor(private smartSearchService: SmartSearchService) {}
  ngOnInit(): void {
    // this.getPagedResults();
  }

  // async search() {
  //   const result = await this.smartSearchService.search(this.query);

  //   if (result.success) {
  //     this.items = result.data
  //   }
  // }

  async getPagedResults() {

    const result = await this.smartSearchService.getPagedResults({
      query: this.query,
      pageNumber: this.paginationParams.pageNumber,
      orderBy: this.paginationParams.orderBy,
      direction: this.paginationParams.direction,
    });

    if (result.success) {
      this.pagedResults = result.data;
      this.items = this.pagedResults.results;
      this.items = result.data.results.map((product) => ({
        ...product,
        image: `https://localhost:7183/${product.image}`,
      }));
    }
    console.log(result)
  }

  /**
   * Ordenar por precio: 0.
   * Ordenar por nombre: 1.
   * Ordenar por ascendente: 0.
   * Ordenar por descendente: 1.
   */

  setOrderBy(choice: number) {
    this.paginationParams.orderBy = choice;
    this.getPagedResults();
  }

  toggleDirection() {
    this.paginationParams.direction = this.paginationParams.direction === 0 ? 1 : 0;
    this.isAscending = this.paginationParams.direction === 0;
    this.getPagedResults();
  }
}
