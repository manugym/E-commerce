import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  StripeEmbeddedCheckout,
  StripeEmbeddedCheckoutOptions,
} from '@stripe/stripe-js';
import { StripeService } from 'ngx-stripe';
import { ProductDto } from '../../models/product-dto';
import { Subscription, timer } from 'rxjs';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { CheckoutService } from '../../services/checkout.service';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit, OnDestroy {
  @ViewChild('checkoutDialog')
  checkoutDialogRef: ElementRef<HTMLDialogElement>;

  product: ProductDto = null;
  sessionId: string = '';
  pollingSubscription: Subscription;
  stripeEmbedCheckout: StripeEmbeddedCheckout;
  temporaryOrderId: number;
  payment: string;

  constructor(
    private checkoutService: CheckoutService,
    private api: ApiService,
    // private pollingService: PollingTemporaryOrdersService,
    private route: ActivatedRoute,
    private router: Router,
    private stripe: StripeService
  ) {}
  ngOnDestroy(): void {
    // this.pollingService.stopPolling(this.pollingSubscription);
    this.pollingSubscription.unsubscribe();
  }

  async ngOnInit() {
    this.temporaryOrderId = this.route.snapshot.queryParamMap.get(
      'temporaryId'
    ) as unknown as number;
    this.payment = this.route.snapshot.queryParamMap.get(
      'payment'
    ) as unknown as string;

    await this.embeddedCheckout();
    this.startPolling();
  }

  // ngOnDestroy(): void {

  // }

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
    const request = await this.checkoutService.getEmbededCheckout(
      this.temporaryOrderId
    );

    if (request.success) {
      const options: StripeEmbeddedCheckoutOptions = {
        clientSecret: request.data.clientSecret,
      };

      this.stripe.initEmbeddedCheckout(options).subscribe((checkout) => {
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
    // console.log(this.temporaryOrderId);
    // await this.pollingService.startPolling(
    //   this.pollingSubscription,
    //   this.temporaryOrderId
    // );
    const pollingInterval = 10000;
    console.log('Refresco...', this.temporaryOrderId);
    this.pollingSubscription = timer(0, pollingInterval).subscribe(() => {
      this.pollingRefresh(this.temporaryOrderId);
    });
  }

  async pollingRefresh(temporaryOrderId: number) {
    await this.api.postPolling(
      `TemporaryOrder/RefreshTemporaryOrders?temporaryOrderId=${temporaryOrderId}`
    );
  }

  cancelCheckoutDialog() {
    this.stripeEmbedCheckout.destroy();
    this.checkoutDialogRef.nativeElement.close;
  }
}
