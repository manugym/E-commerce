import { Component, OnInit } from '@angular/core';
import { Cart } from '../../models/cart';
import { FormsModule } from '@angular/forms';
import { CartDetail } from '../../models/cart-detail';
import { CatalogService } from '../../services/catalog.service';

@Component({
  selector: 'app-local-cart',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './local-cart.component.html',
  styleUrl: './local-cart.component.css',
})
export class LocalCartComponent implements OnInit {
  cart: Cart = {
    id: null,
    userId: null,
    details: [],
  };
  detailIdCounter: number = 0;

  constructor(private catalogService: CatalogService) {}

  ngOnInit(): void {
    this.cart = this.getLocalCart();
  }

  getLocalCart(): Cart {
    const localCart = JSON.parse(localStorage.getItem('localCart'));
    if (!localCart) {
      return { id: null, userId: null, details: [] };
    }

    localCart.details.map(
      (image) =>
        (image.product.image = `https://localhost:7183/${image.product.image}`)
    );

    localCart.details = (localCart.details || []).map((detail: any) => {
      if (detail.product) {
        return detail;
      } else {
        return {
          id: this.detailIdCounter++,
          cartId: localCart.id || null,
          product: detail,
          productId: detail.id,
          amount: 1,
        };
      }
    });

    return localCart;
  }

  saveLocal() {
    localStorage.setItem('localCart', JSON.stringify(this.cart));
  }

  removeProduct(arg0: number) {
    throw new Error('Method not implemented.');
  }
  increaseQuantity(_t11: CartDetail) {
    throw new Error('Method not implemented.');
  }


  /**
   * 
   * Este método está roto, hay que arreglarlo
   */

  async updateQuantity(productId: number, amount: number) {
    const localCart = JSON.parse(localStorage.getItem('localCart'));
    localCart.details.map(
      (image) =>
        (image.product.image = `https://localhost:7183/${image.product.image}`)
    );
    const product = (await this.catalogService.getProductDetailsById(productId))
      .data;

    let cartDetail = this.cart.details.find(
      (detail) => detail.productId === productId
    );

    if (cartDetail) {
      cartDetail.amount = Math.min(cartDetail.amount + amount, product.stock);
    } else {
      this.cart.details.push({
        productId,
        product,
        amount: Math.min(amount, product.stock),
        id: this.detailIdCounter++,
        cartId: this.cart.id,
      });
    }

    this.saveLocal();
  }

  decreaseQuantity(_t11: any) {
    throw new Error('Method not implemented.');
  }

  getTotal() {
    return this.cart.details.reduce(
      (sum, item) => sum + item.product.price * item.amount,
      0
    );
  }
}
