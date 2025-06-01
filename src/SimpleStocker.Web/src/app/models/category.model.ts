import { BaseModel } from "./basemodel.model";

export class Category extends BaseModel {
  name: string = '';
  description: string = '';
  constructor() {
    super();
    this.name = '';
    this.description = '';
  }
}
