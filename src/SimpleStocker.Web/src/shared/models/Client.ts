export class Client {
  id: number
  createdDate: string
  updatedDate: string
  name: string
  email: string
  phoneNumer: string
  address: string
  addressNumber: string
  active: boolean
  birthDate: string

  constructor(init?: Partial<Client>) {
    this.id = init?.id ?? 0
    this.createdDate = init?.createdDate ?? ''
    this.updatedDate = init?.updatedDate ?? ''
    this.name = init?.name ?? ''
    this.email = init?.email ?? ''
    this.phoneNumer = init?.phoneNumer ?? ''
    this.address = init?.address ?? ''
    this.addressNumber = init?.addressNumber ?? ''
    this.active = init?.active ?? false
    this.birthDate = init?.birthDate ?? ''
  }
}
