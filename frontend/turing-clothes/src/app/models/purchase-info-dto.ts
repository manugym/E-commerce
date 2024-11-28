import { TemporaryOrder } from "./temporary-order";

export interface PurchaseInfoDto {
  temporaryOrder: TemporaryOrder;
  totalPrice: number;
  priceInWei: string;
}
