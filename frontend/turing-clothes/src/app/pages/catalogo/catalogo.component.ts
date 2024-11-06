import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule, NgFor } from '@angular/common';
import { PaginationParams } from '../../models/pagination-params';
import { PagedResults } from '../../models/paged-results';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [HeaderComponent, FormsModule, CommonModule],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css',
})
export class CatalogoComponent {
  productList: ProductDto[] = [];
  imageUrl: string[];

  query: string = '';
  items: ProductDto[] = [];

  /**
   * Esto está puesto provisional. REQUIERE REVISIÓN DEL LÍDER.
   */
  isAscending: boolean = true;

  paginationParams: PaginationParams = {
    query: '',
    pageNumber: 1,
    pageSize: 8,
    orderBy: 0, // Default: 0 (por ejemplo, precio)
    direction: 0, // Default: 0 (ascendente)
  };
  
  pagedResults: PagedResults;
  constructor(private catalogService: CatalogService) {}

  ngOnInit() {
    this.getPagedResults();
    this.isAscending = this.paginationParams.direction === 0;
  }

  async getPagedResults() {

    const result = await this.catalogService.getPagedResults({
      query: this.query,
      pageNumber: this.paginationParams.pageNumber,
      pageSize: this.paginationParams.pageSize,
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

  nextPage() {
    if (this.paginationParams.pageNumber < this.pagedResults.totalNumberOfPages) {
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
    return Array.from({ length: this.pagedResults?.totalNumberOfPages }, (_, i) => i + 1);
  }

  goToPage(page: number) {
    if (page !== this.paginationParams.pageNumber) {
      this.paginationParams.pageNumber = page;
      this.getPagedResults();
    }
  }

  onProductsPerPageChange(value: number){
    this.paginationParams.pageSize= value;
    this.paginationParams.pageNumber= 1;
    this.getPagedResults();  
  }

  /*reloadProducts(){
    this.catalogService.getPagedResults(this.paginationParams)
      .then((result) =>{
        this.pagedResults = result.data;
      })
      .catch((error) =>{
        console.error('Error al cargar productos:', error);
      }
      );
  }*/
}
