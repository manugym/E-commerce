import { Component, OnInit } from '@angular/core';
import { CartDetail } from '../../models/cart-detail';
import { Cart } from '../../models/cart';
import { CartServiceService } from '../../services/cart-service.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cartDetail: CartDetail[]
  cart: Cart

  constructor(private cartService: CartServiceService) {}

  ngOnInit(): void {
      this.getCart()
  }

  async getCart() {
    const result = await this.cartService.getCart();
    this.cart = result.data
    console.log(this.cart)
    return result;
  }
}
