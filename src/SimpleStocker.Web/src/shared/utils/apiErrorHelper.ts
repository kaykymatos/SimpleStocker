export function formatApiErrors(apiError: any): string {
  if (apiError && apiError.errors) {
    return apiError.errors
      .map((errDict: Record<string, string>) =>
        Object.entries(errDict)
          .map(([field, msg]) => `${field}: ${msg}`)
          .join(', ')
      )
      .join('\n')
  }
  return apiError?.message || 'Erro ao processar requisição.'
}
