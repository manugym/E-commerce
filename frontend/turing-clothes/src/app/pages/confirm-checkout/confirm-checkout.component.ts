import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Order } from '../../models/order';
import { Subscription } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../models/user-dto';

@Component({
  selector: 'app-confirm-checkout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './confirm-checkout.component.html',
  styleUrl: './confirm-checkout.component.css',
})
export class ConfirmCheckoutComponent implements OnInit {
  routeParamMap$: Subscription;
  // order: Order = {
  //   id: 0,
  //   userId: 0,
  //   paymentMethod: '',
  //   email: '',
  //   transactionStatus: '',
  //   totalPrice: 0,
  //   orderDetails: [],
  // };

  orderDto: UserDto;
  lastOrder: Order;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  async ngOnInit(): Promise<void> {
    this.routeParamMap$ = this.route.paramMap.subscribe(async (paramMap) => {
      // const id = paramMap.get('id') as unknown as number;
      const result = await this.userService.getUserOrder();
      this.orderDto = result.data;
      this.lastOrder = this.orderDto.orders[this.orderDto.orders.length - 1];
      this.lastOrder.orderDetails.map((img) => {
        img.product.image = `${environment.imageUrl}${img.product.image}`;
      });
    });
  }
}
