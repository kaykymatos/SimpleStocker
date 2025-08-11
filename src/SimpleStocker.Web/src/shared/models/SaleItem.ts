export class SaleItem {
  id: number
  createdDate: string
  updatedDate: string
  saleId: number
  productId: number
  quantity: number
  unityPrice: number
  subTotal: number

  constructor(init?: Partial<SaleItem>) {
    this.id = init?.id ?? 0
    this.createdDate = init?.createdDate ?? ''
    this.updatedDate = init?.updatedDate ?? ''
    this.saleId = init?.saleId ?? 0
    this.productId = init?.productId ?? 0
    this.quantity = init?.quantity ?? 0
    this.unityPrice = init?.unityPrice ?? 0
    this.subTotal = init?.subTotal ?? 0
  }
}
