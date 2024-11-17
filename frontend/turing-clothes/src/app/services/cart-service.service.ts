import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Cart } from '../models/cart';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root',
})
export class CartServiceService {
  constructor(private api: ApiService) {}

  async getCart(): Promise<Result<Cart>> {
    const result = await this.api.get<Cart>(`Cart/GetCart`);

    return result;
  }

  async updateQuantity(productId: number, amount: number): Promise<Result<string>> {
    const result = await this.api.post<string>(`Cart/UpdateItem?id=${productId}&amount=${amount}`);
    return result;
  }

  // async increaseQuantity(productId: number): Promise<Result<string>> {
  //   const result = await this.api.put<string>(`Cart/UpdateItem?id=${productId}`);
  //   return result;
  // }

  decreaseQuantity() {
    throw new Error('Method not implemented.');
  }
}
