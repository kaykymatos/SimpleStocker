export class Category {
  id: number
  createdDate: string
  updatedDate: string
  name: string
  description: string

  constructor(init?: Partial<Category>) {
    this.id = init?.id ?? 0
    this.createdDate = init?.createdDate ?? ''
    this.updatedDate = init?.updatedDate ?? ''
    this.name = init?.name ?? ''
    this.description = init?.description ?? ''
  }
}
