export class Product {
  id: number
  createdDate: string
  updatedDate: string
  name: string
  description: string
  quantityStock: number
  unityOfMeasurement: string
  price: number
  categoryId: number
  categoryName: string

  constructor(init?: Partial<Product>) {
    this.id = init?.id ?? 0
    this.createdDate = init?.createdDate ?? ''
    this.updatedDate = init?.updatedDate ?? ''
    this.name = init?.name ?? ''
    this.description = init?.description ?? ''
    this.quantityStock = init?.quantityStock ?? 0
    this.unityOfMeasurement = init?.unityOfMeasurement ?? ''
    this.price = init?.price ?? 0
    this.categoryId = init?.categoryId ?? 0
    this.categoryName = init?.categoryName ?? ''
  }
}
