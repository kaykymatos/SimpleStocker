import { useEffect, useState, useMemo } from 'react'
import { Product } from '../../shared/models/Product'
import { ProductService } from '../../shared/services/ProductService'
import { useNavigate } from 'react-router-dom'

export default function ListProducts() {
  const productService = useMemo(() => new ProductService(), [])
  const [products, setProducts] = useState<Product[]>([])
  const [loading, setLoading] = useState(true)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const navigate = useNavigate()

  useEffect(() => {
    loadProducts()
  }, [productService])

  const loadProducts = () => {
    setLoading(true)
    productService
      .getAll()
      .then((res) => setProducts(res.data ?? []))
      .finally(() => setLoading(false))
  }

  const toggleSelect = (id: number) => {
    setSelectedIds((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    )
  }

  const handleDeleteMany = async (e: React.FormEvent) => {
    if (selectedIds.length === 0) return

    if (
      !window.confirm(
        `Tem certeza que deseja deletar ${selectedIds.length} produto(s)?`
      )
    ) {
      return
    }

    e.preventDefault()
    setLoading(true)
    try {
      await productService.deleteMany(selectedIds)
      setTimeout(() => {
        setLoading(false)
        loadProducts()
        setSelectedIds([])
      }, 1000)
    } catch (err: any) {
      setLoading(false)
      const apiError = err?.response?.data
      alert(apiError.message || 'Erro ao deletar produtos')
    }
  }

  const handleCreate = () => {
    navigate('/products/create')
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3 d-flex justify-content-between align-items-center">
        <h6 className="m-0 font-weight-bold text-primary">Listar Produtos</h6>
        <div>
          <button className="btn btn-info btn-sm mr-1" onClick={handleCreate}>
            <i className="fas fa-plus mr-1"></i> Criar Produto
          </button>
          {selectedIds.length > 0 && (
            <button
              className="btn btn-danger btn-sm"
              onClick={handleDeleteMany}
            >
              <i className="fas fa-trash mr-1"></i> Deletar Selecionados
            </button>
          )}
        </div>
      </div>
      <div className="card-body">
        {loading ? (
          <div>Carregando...</div>
        ) : (
          <div className="table-responsive" style={{ maxHeight: 400 }}>
            <table className="table table-bordered table-hover table-sm mb-0">
              <thead className="thead-light">
                <tr>
                  <th>
                    {/* Checkbox geral */}
                    <input
                      type="checkbox"
                      checked={
                        selectedIds.length === products.length &&
                        products.length > 0
                      }
                      onChange={(e) =>
                        setSelectedIds(
                          e.target.checked ? products.map((p) => p.id) : []
                        )
                      }
                    />
                  </th>
                  <th>ID</th>
                  <th>Nome</th>
                  <th>Descrição</th>
                  <th>Estoque</th>
                  <th>Unidade</th>
                  <th>Preço</th>
                  <th>Categoria</th>
                  <th className="text-center">Ações</th>
                </tr>
              </thead>
              <tbody>
                {products.map((p) => (
                  <tr key={p.id}>
                    <td className="text-center">
                      <input
                        type="checkbox"
                        checked={selectedIds.includes(p.id)}
                        onChange={() => toggleSelect(p.id)}
                      />
                    </td>
                    <td>{p.id}</td>
                    <td>{p.name}</td>
                    <td>{p.description}</td>
                    <td>{p.quantityStock}</td>
                    <td>{p.unityOfMeasurement}</td>
                    <td>
                      {p.price.toLocaleString('pt-BR', {
                        style: 'currency',
                        currency: 'BRL',
                      })}
                    </td>
                    <td>{p.categoryName}</td>
                    <td className="text-center" style={{ minWidth: 120 }}>
                      <button
                        className="btn btn-sm btn-info mr-1"
                        title="Visualizar"
                        onClick={() => navigate(`/products/view/${p.id}`)}
                      >
                        <i className="fas fa-eye"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-warning mr-1"
                        title="Editar"
                        onClick={() => navigate(`/products/update/${p.id}`)}
                      >
                        <i className="fas fa-edit"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-danger"
                        title="Deletar"
                        onClick={() => navigate(`/products/delete/${p.id}`)}
                      >
                        <i className="fas fa-trash"></i>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  )
}
