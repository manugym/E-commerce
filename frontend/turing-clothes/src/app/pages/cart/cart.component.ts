import { Component, OnInit } from '@angular/core';
import { CartDetail } from '../../models/cart-detail';
import { Cart } from '../../models/cart';
import { CartServiceService } from '../../services/cart-service.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CheckoutService } from '../../services/checkout.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent implements OnInit {

  cartDetail: CartDetail[] = [];
  cart: Cart = {
    id: null,
    userId: null,
    details: this.cartDetail
  };

  constructor(private cartService: CartServiceService, private router: Router, private authService: AuthService) {}

  async ngOnInit(): Promise<void> {
    await this.getCart();
  }

  async getCart() {
    const result = await this.cartService.getCart();
    result.data.details.forEach((element) => {
      element.product.image = `https://localhost:7183/${element.product.image}`;
    });
    this.cart = result.data;
  }

  getTotal(): number {
    return this.cart?.details?.reduce((total, item) => {
      return total + item.product.price * item.amount;
    }, 0);
  }

  async updateQuantity(productId: number, amount: number) {
    const result = await this.cartService.updateQuantity(productId, amount);
    if (result.success) {
      return result;
    }
    return result;
  }

  async decreaseQuantity(item: any) {
    item.amount--;

    const result = await this.cartService.updateQuantity(
      item.productId,
      item.amount
    );

    console.log(result);
    if (result.success) {
      await this.getCart();
      return result;
    }
    
    return result;
  }

  async increaseQuantity(item: any) {
    if (item.amount < item.product.stock) {
      item.amount++;
      const result = await this.cartService.updateQuantity(
        item.productId,
        item.amount
      );
      console.log(result);
      if (result.success) {
        await this.getCart();
        return result;
      }
    }
    item.amount = item.product.stock;
    return null;
  }

  async removeProduct(productId: number) {
    const result = await this.cartService.removeProduct(productId);
    if (result.success) {
      this.getCart();
      return result;
    }
    return result;
  }

  async goToCheckout() {
      await this.router.navigate(['/checkout']);
  }
}
