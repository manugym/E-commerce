import { Component, OnInit } from '@angular/core';
import { CheckoutSession } from '../../models/checkout-session';
import { CheckoutSessionStatus } from '../../models/checkout-session-status';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CheckoutService } from '../../services/checkout.service';
import { Order } from '../../models/order';

@Component({
  selector: 'app-confirm-checkout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './confirm-checkout.component.html',
  styleUrl: './confirm-checkout.component.css'
})
export class ConfirmCheckoutComponent implements OnInit {
  order: Order
  // orderItems = [
  //   {
  //     name: 'Camiseta',
  //     quantity: 2,
  //     image: 'https://localhost:7183/images/Camiseta-cuello-redondo-black.png',
  //   },
  //   {
  //     name: 'Pantalón',
  //     quantity: 1,
  //     image: 'https://localhost:7183/images/pantalon-vaquero-hombre.png',
  //   },
  // ];

  constructor(private route: ActivatedRoute, private checkoutService: CheckoutService) {}

  async ngOnInit(): Promise<void> {
    const orderId = +this.route.snapshot.queryParamMap.get('id');
    const result = await this.checkoutService.getOrderById(orderId);
    this.order = result.data;
    console.log(this.order)
    if (this.order) {
      this.order.orderDetails.map(img => {
        img.product.image = `https://localhost:7183/${img.product.image}`;
      })
    }
  }

}
