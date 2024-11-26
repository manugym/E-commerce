import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { CheckoutSession } from '../models/checkout-session';
import { Result } from '../models/result';
import { CheckoutSessionStatus } from '../models/checkout-session-status';
import { ProductDto } from '../models/product-dto';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private api: ApiService) { }

  getAllProducts(temporaryOrderId: number): Promise<Result<ProductDto[]>> {
    return this.api.get<ProductDto[]>(`Checkout/GetAllProductsDto?temporaryOrderId=${temporaryOrderId}`);
  }

  getEmbededCheckout(temporaryOrderId: number): Promise<Result<CheckoutSession>> {
    return this.api.get<CheckoutSession>(`Checkout/embedded?temporaryOrderId=${temporaryOrderId}`);
  }

  getStatus(sessionId: string): Promise<Result<CheckoutSessionStatus>> {
    return this.api.get<CheckoutSessionStatus>(`Checkout/status/${sessionId}`);
  }
}
