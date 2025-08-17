import type { ApiResponse } from '../models/ApiResponse'
import api from './api'

export abstract class BaseService<T> {
  abstract baseUrl: string

  async create(item: T): Promise<ApiResponse<T>> {
    const { data } = await api.post<ApiResponse<T>>(this.baseUrl, item)
    return data
  }

  async update(id: string | number, item: T): Promise<ApiResponse<T>> {
    const { data } = await api.put<ApiResponse<T>>(
      `${this.baseUrl}/${id}`,
      item
    )
    return data
  }

  async getOne(id: string | number): Promise<ApiResponse<T>> {
    const { data } = await api.get<ApiResponse<T>>(`${this.baseUrl}/${id}`)
    return data
  }

  async getAll(): Promise<ApiResponse<T[]>> {
    const { data } = await api.get<ApiResponse<T[]>>(this.baseUrl)
    return data
  }

  async delete(id: string | number): Promise<void> {
    await api.delete(`${this.baseUrl}/${id}`)
  }

  async deleteMany(ids: number[]): Promise<ApiResponse<boolean>> {
    const { data } = await api.delete<ApiResponse<boolean>>(
      `${this.baseUrl}/batch`,
      {
        data: ids,
      } as any
    )
    return data
  }
}
