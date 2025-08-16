import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'
import { useMemo } from 'react'

export default function UpdateCategory() {
  const { id } = useParams()
  const categoryService = useMemo(() => new CategoryService(), [])
  const [form, setForm] = useState<Partial<Category>>({
    name: '',
    description: '',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    if (id) {
      categoryService
        .getOne(id)
        .then((res) => setForm(res.data ?? { name: '', description: '' }))
        .catch(() => setError('Categoria não encontrada.'))
    }
  }, [id, categoryService]) // Added categoryService to dependencies

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      await categoryService.update(id!, form as Category)
      navigate('/categories/list')
    } catch {
      setError('Erro ao atualizar categoria.') // Removed unused variable err
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Editar Categoria</h5>
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
                required
              />
            </div>
            <div className="form-group col-md-6">
              <label>Descrição</label>
              <input
                type="text"
                name="description"
                className="form-control"
                value={form.description}
                onChange={handleChange}
              />
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
