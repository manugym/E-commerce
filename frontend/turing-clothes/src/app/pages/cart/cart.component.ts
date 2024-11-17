import { Component, OnInit } from '@angular/core';
import { CartDetail } from '../../models/cart-detail';
import { Cart } from '../../models/cart';
import { CartServiceService } from '../../services/cart-service.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent implements OnInit {
  cartDetail: CartDetail[];
  cart: Cart;

  constructor(private cartService: CartServiceService) {}

  ngOnInit(): void {
    this.getCart();
  }

  async getCart() {
    const result = await this.cartService.getCart();
    result.data.details.forEach((element) => {
      element.product.image = `https://localhost:7183/${element.product.image}`;
    });
    this.cart = result.data;
    console.log(this.cart.details[0].product);
  }

  getTotal(): number {
    return this.cart?.details?.reduce((total, item) => {
      return total + item.product.price * item.amount;
    }, 0);
  }

  async updateQuantity(productId: number, amount: number) {
    const result = await this.cartService.updateQuantity(productId, amount);
    console.log(result);
    if (result.success) {
      console.log('Producto añadido correctamente');
      return result;
    }
    console.log('Error al actualizar el producto');
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
      console.log('Producto añadido correctamente');
      return result;
    }
    console.log('Error al actualizar el producto');

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
        console.log('Producto actualizado correctamente');
        return result;
      }
      console.log('Error al actualizar el producto');
    }
    item.amount = item.product.stock;
    return null;
  }

  removeProduct() {
    
  }
}
