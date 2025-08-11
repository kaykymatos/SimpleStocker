import { BaseService } from './BaseService'
import { Client } from '../models/Client'

export class ClientService extends BaseService<Client> {
  baseUrl = '/v1/clients'
}
