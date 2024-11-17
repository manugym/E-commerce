import { Injectable, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Cart } from '../models/cart';
import { Result } from '../models/result';
import { CartDetail } from '../models/cart-detail';

@Injectable({
  providedIn: 'root',
})
export class CartServiceService implements OnInit{
  cartQuantity: number
  cartDetail: CartDetail[];
  constructor(private api: ApiService) {}
  ngOnInit(): void {
    this.getCart()
  }

  async getCart(): Promise<Result<Cart>> {
    const result = await this.api.get<Cart>(`Cart/GetCart`);
    this.cartQuantity = result.data.details.reduce((sum, detail) => sum + detail.amount, 0);
    console.log(this.cartQuantity)
    return result;
  }

  async updateQuantity(productId: number, amount: number): Promise<Result<string>> {
    const result = await this.api.post<string>(`Cart/UpdateItem?id=${productId}&amount=${amount}`);
    return result;
  }

  async removeProduct(productId: number): Promise<Result<string>> {
    const result = await this.api.delete<string>(`Cart/RemoveItem?id=${productId}`);
    return result;
  }
  
  async addProductToCart(productId: number): Promise<Result<string>> {
    const result = await this.api.put<string>(`Cart/AddItem?id=${productId}`);
    return result;
  }

  async getCartQuantity(): Promise<number> {
    return await this.cartQuantity
  }
}
