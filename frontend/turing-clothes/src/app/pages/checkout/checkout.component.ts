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
  routeQueryMap$: Subscription;
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
    this.temporaryOrderId = this.route.snapshot.queryParamMap.get('temporaryId') as unknown as number;
    this.sessionId = this.route.snapshot.queryParamMap.get(
      'temporaryId'
    )
    
    this.payment = this.route.snapshot.queryParamMap.get(
      'payment'
    )
    this.startPolling();

    // this.pollingSubscription = this.route.queryParamMap.subscribe(queryMap => this.init(queryMap));

    await this.embeddedCheckout();
    
  }

  // ngOnDestroy(): void {

  // }

  // async init(queryMap: ParamMap) {
  //   this.sessionId = queryMap.get('temporaryId');
  //   console.log(this.sessionId);

  //   if (this.sessionId) {
  //     const request = await this.checkoutService.getStatus(this.sessionId);

  //     if (request.success) {
  //       console.log(request.data.status);
  //     }
  //   } else {
  //     const request = await this.checkoutService.getAllProducts(this.temporaryOrderId);
  //     if (request.success) {
  //       this.product = request.data[0];
  //     }
  //   }
  // }

  async embeddedCheckout() {
    const request = await this.checkoutService.getEmbededCheckout(
      this.temporaryOrderId
    ); 
    this.sessionId = request.data.sessionId;

    if (request.success) {
      const options: StripeEmbeddedCheckoutOptions = {
        onComplete: () => this.goToCorfirmPurchase(this.sessionId),
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
    const pollingInterval = 10000;
    console.log('Refresco...', this.sessionId);
    this.pollingSubscription = timer(0, pollingInterval).subscribe(() => {
      this.pollingRefresh(this.temporaryOrderId);
    });
  }

  async pollingRefresh(temporaryOrderId: number) {
    await this.api.postPolling(
      `TemporaryOrder/RefreshTemporaryOrders?temporaryOrderId=${temporaryOrderId}`
    );
  }

  async goToCorfirmPurchase(sessionId: string) {
    
    await this.checkoutService.getStatus(sessionId, this.temporaryOrderId);
    this.checkoutDialogRef.nativeElement.close;
    this.router.navigate(['home'], {
      queryParams: { sessionId: sessionId },
    });
  }

  cancelCheckoutDialog() {
    this.stripeEmbedCheckout.destroy();
    this.checkoutDialogRef.nativeElement.close;
  }
}
