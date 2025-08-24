import { BaseService } from './BaseService'
import { Category } from '../models/Category'

export class CategoryService extends BaseService<Category> {
  baseUrl = '/v1/categories'
}
