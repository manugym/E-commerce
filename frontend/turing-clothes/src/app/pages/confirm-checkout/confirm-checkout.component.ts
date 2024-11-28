import { Component, OnInit } from '@angular/core';
import { CheckoutSession } from '../../models/checkout-session';
import { CheckoutSessionStatus } from '../../models/checkout-session-status';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CheckoutService } from '../../services/checkout.service';
import { Order } from '../../models/order';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-confirm-checkout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './confirm-checkout.component.html',
  styleUrl: './confirm-checkout.component.css',
})
export class ConfirmCheckoutComponent implements OnInit {
  routeParamMap$: Subscription;
  order: Order = {
    id: 0,
    userId: 0,
    paymentMethod: '',
    email: '',
    transactionStatus: '',
    totalPrice: 0,
    orderDetails: [],
  };

  constructor(
    private route: ActivatedRoute,
    private checkoutService: CheckoutService
  ) {}

  async ngOnInit(): Promise<void> {
    this.routeParamMap$ = this.route.paramMap.subscribe(async (paramMap) => {
      const id = paramMap.get('id') as unknown as number;
      const result = await this.checkoutService.getOrderById(id);
      this.order = result.data;
      this.order.orderDetails.map((img) => {
        img.product.image = `https://localhost:7183/${img.product.image}`;
      });
    });
  }
}
