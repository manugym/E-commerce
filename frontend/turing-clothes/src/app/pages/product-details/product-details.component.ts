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
import { Result } from '../../models/result';
import { CartServiceService } from '../../services/cart-service.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {

  product: ProductDto | null = null;
  reviewText: string = '';

  constructor(private activatedRoute: ActivatedRoute, private catalogService: CatalogService, public authService: AuthService, private cartService: CartServiceService) {}

  async ngOnInit(): Promise<void> {
    const id = this.activatedRoute.snapshot.paramMap.get('id') as unknown as number;

    const result = await this.catalogService.getProductDetailsById(id);
    
    result.data.image = `https://localhost:7183/${result.data.image}`;
    this.product = result.data;
    console.log(this.product);
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

  async addProductToCart(productId: number): Promise<Result<string>> {
    const result = await this.cartService.addProductToCart(productId);
    console.log(result);
    if (result.success) {
      console.log('Producto actualizado correctamente');
      await this.cartService.getCart();
      return result;
    }
    console.log('Error al actualizar el producto');
    return result;
  }
  
}
