export function getApiFieldErrors(apiError: any): Record<string, string> {
  const fieldErrors: Record<string, string> = {}
  if (apiError && apiError.errors) {
    apiError.errors.forEach((errDict: Record<string, string>) => {
      Object.entries(errDict).forEach(([field, msg]) => {
        fieldErrors[field] = msg
      })
    })
  }
  return fieldErrors
}
