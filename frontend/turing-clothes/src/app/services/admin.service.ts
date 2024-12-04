import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { User } from '../models/user';
import { ProductDto } from '../models/product-dto';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private api: ApiService) {}

  async getProducts(): Promise<Result<ProductDto[]>> {
    const products = await this.api.get<ProductDto[]>('Admin/getAllProducts');
      return products;
  }

  async getUsers(): Promise<Result<User[]>> {
    const users = await this.api.get<User[]>('Admin/getAllUsers');
      return users;
  }

  async addProduct(product: ProductDto): Promise<Result<void>> {
    const result = await this.api.post<void>('Admin/addProduct', product);
      return result;
  }

  async removeProduct(productId: number): Promise<Result<void>> {
    const result = await this.api.delete<void>(`Admin/deleteProduct/${productId}`);
      return result;
  }
}