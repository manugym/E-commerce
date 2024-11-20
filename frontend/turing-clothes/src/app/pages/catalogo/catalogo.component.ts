import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule, NgFor } from '@angular/common';
import { PaginationParams } from '../../models/pagination-params';
import { PagedResults } from '../../models/paged-results';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css',
})
export class CatalogoComponent implements OnInit {
  productList: ProductDto[] = [];
  imageUrl: string[];
  items: ProductDto[] = [];
  isAscending: boolean = true;
  oldQuery: string;
  paginationParams: PaginationParams;
  pagedResults: PagedResults;

  constructor(private catalogService: CatalogService) {}

  ngOnInit() {
    const savedSettings = this.catalogService.getUserSettings();

    this.paginationParams = savedSettings;
    this.oldQuery = this.paginationParams.query;

    this.getPagedResults();
    this.isAscending = this.paginationParams.direction === 0;
  }

  async getPagedResults() {
    if (this.oldQuery !== this.paginationParams.query) {
      this.paginationParams.pageNumber = 1;
    }

    this.oldQuery = this.paginationParams.query;

    const result = await this.catalogService.getPagedResults(
      this.paginationParams
    );

    if (result.success) {
      this.pagedResults = result.data;
      this.paginationParams.pageNumber = result.data.pageNumber;
      this.items = this.pagedResults.results;
      this.items = result.data.results.map((product) => ({
        ...product,
        image: `https://localhost:7183/${product.image}`,
      }));
    }
  }

  /**
   * Ordenar por precio: 0.
   * Ordenar por nombre: 1.
   * Ordenar por ascendente: 0.
   * Ordenar por descendente: 1.
   */

  setOrderBy(choice: number) {
    this.paginationParams.orderBy = choice;
    this.paginationParams.pageNumber = 1;
    this.getPagedResults();
  }

  toggleDirection() {
    this.paginationParams.direction =
      this.paginationParams.direction === 0 ? 1 : 0;
    this.isAscending = this.paginationParams.direction === 0;
    this.paginationParams.pageNumber = 1;
    this.getPagedResults();
  }

  nextPage() {
    if (
      this.paginationParams.pageNumber < this.pagedResults.totalNumberOfPages
    ) {
      this.paginationParams.pageNumber++;
      this.getPagedResults();
    }
  }

  previousPage() {
    if (this.paginationParams.pageNumber > 1) {
      this.paginationParams.pageNumber--;
      this.getPagedResults();
    }
  }

  getPageNumbers(): number[] {
    return Array.from(
      { length: this.pagedResults?.totalNumberOfPages },
      (_, i) => i + 1
    );
  }

  goToPage(page: number) {
    if (page !== this.paginationParams.pageNumber) {
      this.paginationParams.pageNumber = page;
      this.getPagedResults();
    }
  }

  onProductsPerPageChange(value: number) {
    this.paginationParams.pageSize = value;
    this.getPagedResults();
  }
}
