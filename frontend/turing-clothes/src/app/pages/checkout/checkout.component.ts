import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { StripeEmbeddedCheckout, StripeEmbeddedCheckoutOptions } from '@stripe/stripe-js';
import { StripeService } from 'ngx-stripe';
import { ProductDto } from '../../models/product-dto';
import { Subscription } from 'rxjs';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { CheckoutService } from '../../services/checkout.service';
import { PollingTemporaryOrdersService } from '../../services/polling-temporary-orders.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent implements OnInit{
  @ViewChild('checkoutDialog')
  checkoutDialogRef: ElementRef<HTMLDialogElement>;

  product: ProductDto = null;
  sessionId: string = '';
  routeQueryMap$: Subscription;
  stripeEmbedCheckout: StripeEmbeddedCheckout;
  temporaryOrderId: number;
  payment: string;

  constructor(
    private checkoutService: CheckoutService,
    private pollingService: PollingTemporaryOrdersService,
    private route: ActivatedRoute,
    private router: Router,
    private stripe: StripeService) { }

  async ngOnInit() {
    this.temporaryOrderId = this.route.snapshot.queryParamMap.get(
        'temporaryId') as unknown as number;
    this.payment = this.route.snapshot.queryParamMap.get('payment') as unknown as string;
    
    await this.embeddedCheckout();
    this.startPolling();
    
  }

  ngOnDestroy(): void {
    // Cuando este componente se destruye hay que cancelar la suscripción.
    // Si no se cancela se seguirá llamando aunque el usuario no esté ya en esta página
    this.routeQueryMap$.unsubscribe();
  }

  // async init(queryMap: ParamMap) {
  //   this.sessionId = queryMap.get('session_id');

  //   if (this.sessionId) {
  //     const request = await this.service.getStatus(this.sessionId);

  //     if (request.success) {
  //       console.log(request.data);
  //     }
  //   } else {
  //     const request = await this.service.getAllProducts(this.temporaryOrderId);
  //     if (request.success) {
  //       this.product = request.data[0];
  //     }
  //   }
  // }

  async embeddedCheckout() {
    const request = await this.checkoutService.getEmbededCheckout(this.temporaryOrderId);

    if (request.success) {
      const options: StripeEmbeddedCheckoutOptions = {
        clientSecret: request.data.clientSecret
      };

      this.stripe.initEmbeddedCheckout(options)
        .subscribe((checkout) => {
          this.stripeEmbedCheckout = checkout;
          checkout.mount('#checkout');
          this.checkoutDialogRef.nativeElement.showModal;
        });
    }
  }

  reload() {
    this.router.navigate(['checkout']);
  }

  async startPolling() {
    console.log(this.temporaryOrderId)
    await this.pollingService.startPolling(this.temporaryOrderId);
  }

  cancelCheckoutDialog() {
    this.stripeEmbedCheckout.destroy();
    this.checkoutDialogRef.nativeElement.close;
  }
}
