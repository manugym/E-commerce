import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule, NgFor } from '@angular/common';
import { PaginationParams } from '../../models/pagination-params';
import { PagedResults } from '../../models/paged-results';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ReviewDto } from '../../models/review-dto';

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
  productReviews: { [key: number]: number } = {};

  constructor(private catalogService: CatalogService) { }

  ngOnInit() {
    const savedSettings = this.catalogService.getUserSettings();

    this.paginationParams = savedSettings;
    this.oldQuery = this.paginationParams.query;

    this.loadProductsAndReviews();

    this.getPagedResults();
    this.isAscending = this.paginationParams.direction === 0;
  }

  loadProductsAndReviews(): void {
    this.items.forEach((product) => {
      this.loadProductReviews(product.id);
    });
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
  } catch(error) {
    console.error('Error al obtener los productos:', error);
  }

  async loadProductReviews(productId: number) {
    const reviews = await this.catalogService.getProductReviews(productId)
    if (typeof reviews === 'number') {
      this.productReviews[productId] = reviews;
      console.log(`Product ID: ${productId}, Directly Assigned Average Rating: ${reviews}`);
    } else if (reviews && reviews.data.length > 0) {
      const totalRating = reviews.data.reduce((acc, review) => acc + (review.rating || 0), 0);
      const averageRating = totalRating / reviews.data.length;

      this.productReviews[productId] = averageRating;
      console.log(`Product ID: ${productId}, Calculated Average Rating: ${averageRating}`);
    } else {
      this.productReviews[productId] = 0;
      console.log(`Product ID: ${productId} has no reviews, setting rating to 0`);
    }
  }


  getStarArray(rating: number): number[] {
    if (isNaN(rating) || rating < 0) {
      rating = 0;
    }

    const filledStars = Math.floor(rating);
    const emptyStars = 5 - filledStars;
    const halfStar = rating % 1 > 0.5 ? 1 : 0;

    const stars = Array(filledStars).fill(1);
    if (halfStar) stars.push(0.5);
    while (stars.length < 5) {
      stars.push(0);
    }

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

