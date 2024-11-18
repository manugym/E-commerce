import { Injectable, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Cart } from '../models/cart';
import { Result } from '../models/result';
import { CartDetail } from '../models/cart-detail';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartServiceService implements OnInit{
  
  cartDetail: CartDetail[];
  private cartQuantitySubject = new BehaviorSubject<number>(0);
  cartQuantity$ = this.cartQuantitySubject.asObservable();
  
  constructor(private api: ApiService) {}
  ngOnInit(): void {
    this.getCart()
  }

  async getCart(): Promise<Result<Cart>> {
    const result = await this.api.get<Cart>(`Cart/GetCart`);
    //this.cartQuantity = result.data.details.reduce((sum, detail) => sum + detail.amount, 0);
    const quantity = result.data.details.reduce((sum, detail) => sum + detail.amount, 0);
    this.cartQuantitySubject.next(quantity);
    return result;
  }

  async updateQuantity(productId: number, amount: number): Promise<Result<string>> {
    const result = await this.api.post<string>(`Cart/UpdateItem?id=${productId}&amount=${amount}`);

    return result;
  }

  async removeProduct(productId: number): Promise<Result<string>> {
    const result = await this.api.delete<string>(`Cart/RemoveItem?id=${productId}`);
    await this.getCart();
    return result;
  }
  
  async addProductToCart(productId: number, quantity: number): Promise<Result<string>> {
    const result = await this.api.put<string>(`Cart/AddItem?id=${productId}&quantity=${quantity}`);
    return result;
  }

}
