import { BaseService } from './BaseService'
import { Product } from '../models/Product'

export class ProductService extends BaseService<Product> {
  baseUrl = '/v1/products'
}
