import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { ReviewDto } from '../../models/review-dto';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { User } from '../../models/user';
import { ChangeDetectorRef } from '@angular/core';



@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {
  items: ProductDto[] = [];
  product: ProductDto | null = null;
  reviewText: string = '';
  productReviews: { [key: number]: number } = {}

  constructor(private activatedRoute: ActivatedRoute, private catalogService: CatalogService, public authService: AuthService) {}

  async ngOnInit(): Promise<void> {
    const id = this.activatedRoute.snapshot.paramMap.get('id') as unknown as number;
  
    const result = await this.catalogService.getProductDetailsById(id);
  
    // Verifica si la URL ya contiene "http" o "https" para evitar duplicados
    if (!result.data.image.startsWith('http')) {
      result.data.image = `https://localhost:7183/${result.data.image}`;
    }
  
    this.product = result.data;
    this.loadProductsAndReviews();
    console.log(this.product);
  }
  
  //esto duplicaba la url de la imagen, he hecho la versión de arriba que distingue el http y el https para que en caso del primero no la duplique
  // async ngOnInit(): Promise<void> {
  //   const id = this.activatedRoute.snapshot.paramMap.get('id') as unknown as number;

  //   const result = await this.catalogService.getProductDetailsById(id);
    
  //   result.data.image = `https://localhost:7183/${result.data.image}`;
  //   this.product = result.data;
  //   this.loadProductsAndReviews();
  //   console.log(this.product);
  // }

  loadProductsAndReviews(): void {
    this.loadProductReviews(this.product.id);

  }

  loadProductReviews(productId: number): void {
    this.catalogService.getProductReviews(productId).subscribe(
      (reviews: ReviewDto[]) => {  
        if (reviews && reviews.length > 0) {
          const totalRating = reviews.reduce((acc, review) => acc + (review.rating || 0), 0);
          const averageRating = totalRating / reviews.length;
          
          this.productReviews[productId] = averageRating;
          console.log(`Product ID: ${productId}, Calculated Average Rating: ${averageRating}`);
        } else {
          this.productReviews[productId] = 0;
          console.log(`Product ID: ${productId} has no reviews, setting rating to 0`);
        }
      },
      (error) => {
        console.error(`Error al obtener reseñas para el producto ${productId}:`, error);
        this.productReviews[productId] = 0;
      }
    );
  }
  

  getStarClasses(rating: number): string[] {
    const classes = [];
    
    if (rating === 1) {
      // Buena: cinco estrellas completas
      classes.push('bi bi-star-fill', 'bi bi-star-fill', 'bi bi-star-fill', 'bi bi-star-fill', 'bi bi-star-fill');
    } else if (rating === 0) {
      // Regular: tres estrellas completas y dos vacías
      classes.push('bi bi-star-fill', 'bi bi-star-fill', 'bi bi-star-fill', 'bi bi-star', 'bi bi-star');
    } else if (rating === -1) {
      // Mala: cinco estrellas vacías
      classes.push('bi bi-star', 'bi bi-star', 'bi bi-star', 'bi bi-star', 'bi bi-star');
    }
  
    return classes;
  }
  

  async confirmReview(): Promise<void> {
    if (!this.product) return;

    const review: ReviewDto = {
      productId: this.product.id,
      texto: this.reviewText,
      id: 0,
      userId: '',
      rating: 0,
      dateTime: '',
      user: new User,
      product: undefined
    };
    try {
      const result = await this.catalogService.addReview(review);
      if (result.success) {
        Swal.fire('Reseña enviada');
        this.reviewText = '';
        this.product.reviews.push(result.data);
      } else{
        Swal.fire('No se pudo enviar la reseña')
      }
    } catch {
      Swal.fire('Error')
    }
  }
}
