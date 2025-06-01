import { BaseModel } from './basemodel.model';

export class SaleItem extends BaseModel {
  saleId: number = 0;
  productId: number = 0;
  quantity: number = 0;
  unityPrice: number = 0;
  subTotal: number = 0;

  constructor() {
    super();
    this.saleId = 0;
    this.productId = 0;
    this.quantity = 0;
    this.unityPrice = 0;
    this.subTotal = 0;
  }
}
