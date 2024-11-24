// import { Injectable, OnInit } from '@angular/core';
// import { ApiService } from './api.service';
// import { Subscription, timer } from 'rxjs';

// @Injectable({
//   providedIn: 'root',
// })
// export class PollingTemporaryOrdersService {
//   pollingInterval: number = 10000;

//   constructor(private api: ApiService) {}

//   startPolling(pollingSubscription: Subscription, temporaryOrderId: number) {
//     pollingSubscription = timer(0, this.pollingInterval).subscribe(() => {
//       this.pollingRefresh(temporaryOrderId);
//     });
//   }
//   async pollingRefresh(temporaryOrderId: number) {
//     console.log(temporaryOrderId);
//     await this.api.postPolling(
//       `TemporaryOrder/RefreshTemporaryOrders?temporaryOrderId=${temporaryOrderId}`
//     );
//   }

//   stopPolling(pollingSubscription: Subscription) {
//       pollingSubscription.unsubscribe();
//   }
// }

/**
 * LO INTENTÉ EN UN SERVICIO PERO NO HUBO MANERA
 */
