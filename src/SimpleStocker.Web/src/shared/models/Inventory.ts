export class Inventory {
  id: number
  productId: number
  quantity: number

  constructor(init?: Partial<Inventory>) {
    this.id = init?.id ?? 0
    this.productId = init?.productId ?? 0
    this.quantity = init?.quantity ?? 0
  }
}
