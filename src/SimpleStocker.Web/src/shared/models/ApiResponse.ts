export class ApiResponse<T> {
  success: boolean
  message: string
  data?: T
  errors: Array<{ [field: string]: string }>

  constructor(init?: Partial<ApiResponse<T>>) {
    this.success = init?.success ?? false
    this.message = init?.message ?? ''
    this.data = init?.data
    this.errors = init?.errors ?? []
  }
}
