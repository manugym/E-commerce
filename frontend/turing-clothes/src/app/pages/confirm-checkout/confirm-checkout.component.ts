import { Component, OnInit } from '@angular/core';
import { CheckoutSession } from '../../models/checkout-session';
import { CheckoutSessionStatus } from '../../models/checkout-session-status';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-confirm-checkout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './confirm-checkout.component.html',
  styleUrl: './confirm-checkout.component.css'
})
export class ConfirmCheckoutComponent implements OnInit {
  data: CheckoutSessionStatus
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

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    const resultJson = this.route.snapshot.queryParamMap.get('result');
    if (resultJson) {
      this.data = JSON.parse(resultJson) as CheckoutSessionStatus;
      this.data.order.orderDetails.map(img => {
        img.product.image = `https://localhost:7183/${img.product.image}`;
      })
    }
  }

}
