import type { SaleItem } from './SaleItem'

export class Sale {
  id: number
  createdDate: string
  updatedDate: string
  items: SaleItem[]
  totalAmount: number
  clientId: number
  discount: number
  paymentMethod: number
  status: number

  constructor(init?: Partial<Sale>) {
    this.id = init?.id ?? 0
    this.createdDate = init?.createdDate ?? ''
    this.updatedDate = init?.updatedDate ?? ''
    this.items = init?.items ?? []
    this.totalAmount = init?.totalAmount ?? 0
    this.clientId = init?.clientId ?? 0
    this.discount = init?.discount ?? 0
    this.paymentMethod = init?.paymentMethod ?? 0
    this.status = init?.status ?? 0
  }
}
