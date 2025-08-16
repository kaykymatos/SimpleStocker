import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'
import { useMemo } from 'react'
import { getApiFieldErrors } from '../../shared/utils/apiErrorFieldHelper'

export default function CreateCategory() {
  const categoryService = useMemo(() => new CategoryService(), [])
  const [form, setForm] = useState<Partial<Category>>({
    name: '',
    description: '',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({})
  const navigate = useNavigate()

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = e.target
    setForm((prev) => ({
      ...prev,
      [name]: value,
    }))
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      await categoryService.create(form as Category)
      navigate('/categories/list')
    } catch (err: any) {
      const apiError = err?.response?.data
      setFieldErrors(getApiFieldErrors(apiError))
      console.log('Teste', apiError)
      if (apiError?.message) setError(apiError?.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Criar Categoria</h5>
      </div>
      <div className="card-body">
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group col-md-6">
              <label>Nome</label>
              <input
                type="text"
                name="name"
                className="form-control"
                value={form.name}
                onChange={handleChange}
                placeholder="Digite o nome da categoria"
              />
              {fieldErrors.Name && (
                <div className="text-danger small mt-1">{fieldErrors.Name}</div>
              )}
            </div>
            <div className="form-group col-md-6">
              <label>Descrição</label>
              <input
                type="text"
                name="description"
                className="form-control"
                value={form.description}
                onChange={handleChange}
                placeholder="Digite a descrição"
              />
              {fieldErrors.Description && (
                <div className="text-danger small mt-1">
                  {fieldErrors.Description}
                </div>
              )}
            </div>
          </div>
          {error && <div className="alert alert-danger">{error}</div>}
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? 'Salvando...' : 'Salvar'}
          </button>
        </form>
      </div>
    </div>
  )
}
