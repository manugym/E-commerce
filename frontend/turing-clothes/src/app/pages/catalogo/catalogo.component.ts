import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule, NgFor } from '@angular/common';
import { PaginationParams } from '../../models/pagination-params';
import { PagedResults } from '../../models/paged-results';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ReviewDto } from '../../models/review-dto';
import { lastValueFrom } from 'rxjs';


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
  productReviews: { [productid: number]: number } = {};

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
      this.items.forEach((product) => this.loadProductReviews(product.id));
    }
  } catch (error) {
    console.error('Error al obtener los productos:', error);
  }

  loadProductReviews(productId: number): void {
    
    this.items.forEach((product) => {this.catalogService.getProductReviews(productId).subscribe(
      (reviews: ReviewDto[]) => {
        console.log(`Reviews for product ID ${productId}:`, reviews); // Muestra los datos de reseñas
        if (reviews && reviews.length > 0) {
          const averageRating = reviews.reduce((acc, review) => acc + review.rating, 0) / reviews.length;
          this.productReviews[product.id] = averageRating;  // Asegúrate de que esto está actualizando el valor
          console.log('Ratings en productReviews:', this.productReviews);
          
        } else {
          this.productReviews[productId] = 0; // Establece 0 si no hay reseñas
        }
      },
      (error) => {
        console.error(`Error fetching reviews for product ID ${productId}:`, error);
        this.productReviews[productId] = 0; // Establece 0 en caso de error
      }
    );})
    
}

getStarArray(rating: number): number[] {
  
  // Rounding down the rating to the nearest integer
  const filledStars = Math.floor(rating);  // Número de estrellas llenas
  const emptyStars = 5 - filledStars;      // Número de estrellas vacías
  const halfStar = rating % 1 > 0.5 ? 1 : 0;  // Determinamos si hay medio estrella

  console.log('Rating:', rating);
  console.log('Filled Stars:', filledStars);
  console.log('Half Star:', halfStar);
  console.log('Empty Stars:', emptyStars);

  const stars = [];
  for (let i = 0; i < filledStars; i++) {
    stars.push(1);  // Estrella llena
  }
  if (halfStar) stars.push(0.5);  // Medio estrella si es necesario
  for (let i = 0; i < emptyStars; i++) {
    stars.push(0);  // Estrella vacía
  }

  console.log('Stars array:', stars);
  return stars;
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

  round(value: number): number {
    return Math.round(value);
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
  
