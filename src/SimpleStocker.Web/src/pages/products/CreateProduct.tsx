import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { Product } from '../../shared/models/Product'
import { ProductService } from '../../shared/services/ProductService'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'
import { useMemo } from 'react'
import { getApiFieldErrors } from '../../shared/utils/apiErrorFieldHelper'

export default function CreateProduct() {
  const productService = new ProductService()
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
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({})
  const navigate = useNavigate()

  useEffect(() => {
    categoryService
      .getAll()
      .then((res) => setCategories(res.data ?? []))
      .catch(() => setCategories([]))
  }, [categoryService])

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
      await productService.create(form as Product)
      navigate('/products/list')
    } catch (err: any) {
      const apiError = err?.response?.data
      setFieldErrors(getApiFieldErrors(apiError))
      if (apiError?.message) setError(apiError?.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Criar Produto</h5>
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
                  placeholder="Digite o nome do produto"
                  className="form-control"
                />
                {fieldErrors.Name && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.Name}
                  </div>
                )}
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
                  placeholder="Digite a descrição"
                  className="form-control"
                />
                {fieldErrors.Description && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.Description}
                  </div>
                )}
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
                  placeholder="Digite o preço"
                  className="form-control"
                />
                {fieldErrors.Price && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.Price}
                  </div>
                )}
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
                  placeholder="Digite a quantidade em estoque"
                  className="form-control"
                />
                {fieldErrors.QuantityStock && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.QuantityStock}
                  </div>
                )}
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
                  placeholder="Digite a unidade de medida"
                  className="form-control"
                />
                {fieldErrors.UnityOfMeasurement && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.UnityOfMeasurement}
                  </div>
                )}
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
                  <option value={0} disabled>
                    Selecione uma categoria
                  </option>
                  {categories.map((cat) => (
                    <option key={cat.id} value={cat.id}>
                      {cat.name}
                    </option>
                  ))}
                </select>
                {fieldErrors.CategoryId && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.CategoryId}
                  </div>
                )}
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
