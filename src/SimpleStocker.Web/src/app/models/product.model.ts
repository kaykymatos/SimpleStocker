import { BaseModel } from "./basemodel.model";

export class Product extends BaseModel {
    name:string;
    description:string;
    quantityStock:number;
    unityOfMeasurement:string;
    price:number;
    categoryId:number;
    categoryName:string;

    constructor() {
        super();
        this.name = '';
        this.description = '';
        this.quantityStock = 0;
        this.unityOfMeasurement = '';
        this.price = 0;
        this.categoryId = 0;
        this.categoryName = '';
    }
}