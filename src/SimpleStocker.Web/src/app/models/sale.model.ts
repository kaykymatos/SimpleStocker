import { BaseModel } from './basemodel.model';
import { EPaymentMethod } from './enums/ePaymentMethod';
import { ESaleStatus } from './enums/eSaleStatus';
import { SaleItem } from './saleItem.model';

export class Sale extends BaseModel {
  items: SaleItem[] = [];
  totalAmount: number = 0;
  customerId: number = 0;
  discount: number = 0;
  paymentMethod: EPaymentMethod = EPaymentMethod.Pix;
  status: ESaleStatus = ESaleStatus.Pending;

  constructor() {
    super();
    this.customerId = 0;
    this.discount = 0;
    this.paymentMethod = EPaymentMethod.Pix;
    this.status = ESaleStatus.Pending;
    this.items = [];
  }
}
