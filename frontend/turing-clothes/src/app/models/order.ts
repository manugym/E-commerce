import { CartDetail } from "./cart-detail";

export interface Order {
  id: number,
  userId: number,
  paymentMethod: string,
  transactionStatus: string,
  totalPrice: number,
  orderDetails: CartDetail[]
}
