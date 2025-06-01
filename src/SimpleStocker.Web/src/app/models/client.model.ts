import { BaseModel } from './basemodel.model';

export class Client extends BaseModel {
  name: string = '';
  email: string = '';
  phoneNumber: string = '';
  address: string = '';
  addressNumber: string = '';
  active: boolean = true;
  birthDate: Date = new Date();

  constructor() {
    super();
    this.name = '';
    this.email = '';
    this.phoneNumber = '';
    this.address = '';
    this.addressNumber = '';
    this.active = false;
    this.birthDate = new Date();
  }
}
