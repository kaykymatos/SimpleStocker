import { useEffect, useMemo, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Product } from '../../shared/models/Product'
import { ProductService } from '../../shared/services/ProductService'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'

export default function UpdateProduct() {
  const { id } = useParams()
  const productService = useMemo(() => new ProductService(), [])
  const categoryService = useMemo(() => new CategoryService(), [])
  const [categories, setCategories] = useState<Category[]>([])
  const [form, setForm] = useState<Partial<Product>>({
    name: '',
    description: '',
    price: 0,
    quantityStock: 0,
    unityOfMeasurement: '',
    categoryId: 0,
    categoryName: '',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    if (id) {
      productService
        .getOne(id)
        .then((res) =>
          setForm(
            res.data ?? {
              name: '',
              description: '',
              price: 0,
              quantityStock: 0,
              unityOfMeasurement: '',
              categoryId: 0,
              categoryName: '',
            }
          )
        )
        .catch(() => setError('Produto não encontrado.'))
    }
    categoryService
      .getAll()
      .then((res) => setCategories(res.data ?? []))
      .catch(() => setCategories([]))
  }, [id, categoryService, productService])

  function handleChange(
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) {
    const { name, value, type } = e.target
    if (type === 'checkbox' && 'checked' in e.target) {
      setForm((prev) => ({
        ...prev,
        [name]: (e.target as HTMLInputElement).checked,
      }))
    } else {
      setForm((prev) => ({ ...prev, [name]: value }))
    }
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      await productService.update(id!, form as Product)
      navigate('/products/list')
    } catch {
      setError('Erro ao atualizar produto.') // Error handling
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Atualizar Produto</h5>
      </div>
      <div className="card-body">
        {error && <div className="alert alert-danger">{error}</div>}
        <form onSubmit={handleSubmit}>
          <div className="row">
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="name">Nome do Produto</label>
                <input
                  id="name"
                  name="name"
                  value={form.name ?? ''}
                  onChange={handleChange}
                  placeholder="Nome"
                  className="form-control"
                />
              </div>
            </div>
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="description">Descrição</label>
                <input
                  id="description"
                  name="description"
                  value={form.description ?? ''}
                  onChange={handleChange}
                  placeholder="Descrição"
                  className="form-control"
                />
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="price">Preço</label>
                <input
                  id="price"
                  name="price"
                  type="number"
                  value={form.price ?? 0}
                  onChange={handleChange}
                  placeholder="Preço"
                  className="form-control"
                />
              </div>
            </div>
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="quantityStock">Estoque</label>
                <input
                  id="quantityStock"
                  name="quantityStock"
                  type="number"
                  value={form.quantityStock ?? 0}
                  onChange={handleChange}
                  placeholder="Estoque"
                  className="form-control"
                />
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="unityOfMeasurement">Unidade de Medida</label>
                <input
                  id="unityOfMeasurement"
                  name="unityOfMeasurement"
                  value={form.unityOfMeasurement ?? ''}
                  onChange={handleChange}
                  placeholder="Unidade de Medida"
                  className="form-control"
                />
              </div>
            </div>
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="categoryId">Categoria</label>
                <select
                  id="categoryId"
                  name="categoryId"
                  value={form.categoryId ?? 0}
                  onChange={handleChange}
                  className="form-control"
                >
                  <option value={0}>Selecione uma categoria</option>
                  {categories.map((cat) => (
                    <option key={cat.id} value={cat.id}>
                      {cat.name}
                    </option>
                  ))}
                </select>
              </div>
            </div>
          </div>
          <button
            className="btn btn-primary mt-3"
            type="submit"
            disabled={loading}
          >
            Salvar
          </button>
        </form>
      </div>
    </div>
  )
}
