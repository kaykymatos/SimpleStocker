export class ApiResponse<T> {
  success: boolean
  message: string
  data?: T

  constructor(init?: Partial<ApiResponse<T>>) {
    this.success = init?.success ?? false
    this.message = init?.message ?? ''
    this.data = init?.data
  }
}
