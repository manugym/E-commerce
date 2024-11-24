import { Injectable, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Subscription, timer } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PollingTemporaryOrdersService implements OnInit {
  pollingInterval: number = 10000;
  pollingSubscription: Subscription;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.stopPolling();
  }

  startPolling(temporaryOrderId: number) {
    this.pollingSubscription = timer(0, this.pollingInterval).subscribe(() => {
      this.pollingRefresh(temporaryOrderId);
    });
  }
  async pollingRefresh(temporaryOrderId: number) {
    console.log(temporaryOrderId)
    await this.api.postPolling('TemporaryOrder/RefreshTemporaryOrders', temporaryOrderId);
  }

  stopPolling() {
    if (this.pollingSubscription) {
      this.pollingSubscription.unsubscribe();
    }
  }
}
