export class BaseModel {
  id: number=0;
  createdDate: Date=new Date();
  updatedDate: Date=new Date();

  constructor(){
    this.id = 0;
    this.createdDate=new Date();
    this.updatedDate=new Date();
  }
}
