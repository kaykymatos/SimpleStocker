import { BaseService } from './BaseService'
import { Inventory } from '../models/Inventory'

export class InventoryService extends BaseService<Inventory> {
  baseUrl = '/v1/inventory'
}
