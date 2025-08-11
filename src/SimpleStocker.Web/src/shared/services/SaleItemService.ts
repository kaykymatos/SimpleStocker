import { BaseService } from './BaseService'
import { SaleItem } from '../models/SaleItem'

export class SaleItemService extends BaseService<SaleItem> {
  baseUrl = '/v1/saleitems'
}
