import { BaseService } from './BaseService'
import { Sale } from '../models/Sale'

export class SaleService extends BaseService<Sale> {
  baseUrl = '/v1/sales'
}
