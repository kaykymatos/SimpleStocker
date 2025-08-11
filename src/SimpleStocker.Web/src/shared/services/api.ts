import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000, // 10 segundos
})

// Interceptor para adicionar token de autenticação, se existir
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers = config.headers || {}
      config.headers['Authorization'] = `Bearer ${token}`
    }
    return config
  },
  (error) => Promise.reject(error)
)

// Interceptor para tratar respostas e erros globais
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      // Erros de resposta HTTP
      if (error.response.status === 401) {
        // Redireciona para login se não autorizado
        window.location.href = '/login'
      } else if (error.response.status === 403) {
        // Redireciona para página de acesso negado
        window.location.href = '/forbidden'
      } else if (error.response.status === 500) {
        // Redireciona para página de erro interno
        window.location.href = '/error'
      }
    } else if (error.request) {
      // Sem resposta do servidor
      console.error('Sem resposta do servidor')
    } else {
      // Erro de configuração
      console.error('Erro na configuração da requisição')
    }
    return Promise.reject(error)
  }
)

export default api
