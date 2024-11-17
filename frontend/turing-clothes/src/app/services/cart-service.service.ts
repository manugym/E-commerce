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
}