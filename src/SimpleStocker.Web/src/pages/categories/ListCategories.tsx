import { useEffect, useState, useMemo } from 'react'
import { formatDateTime } from '../../shared/utils/dateUtils'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'
import { useNavigate } from 'react-router-dom'

export default function ListCategories() {
  const categoryService = useMemo(() => new CategoryService(), [])
  const [categories, setCategories] = useState<Category[]>([])
  const [loading, setLoading] = useState(true)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const navigate = useNavigate()

  useEffect(() => {
    loadCategories()
  }, [categoryService])

  const loadCategories = () => {
    setLoading(true)
    categoryService
      .getAll()
      .then((res) => setCategories(res.data ?? []))
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
        `Tem certeza que deseja deletar ${selectedIds.length} categoria(s)?`
      )
    ) {
      return
    }
    e.preventDefault()
    setLoading(true)
    try {
      await categoryService.deleteMany(selectedIds)
      setTimeout(() => {
        setLoading(false)
        loadCategories();
        setSelectedIds([])
      }, 1000)
    } catch (err: any) {
      setLoading(false)

      const apiError = err?.response?.data
      alert(apiError.message || 'Erro ao deletar categorias')
    }
  }

  const handleCreate = () => {
    navigate('/categories/create')
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3 d-flex justify-content-between align-items-center">
        <h6 className="m-0 font-weight-bold text-primary">Listar Categorias</h6>
        <div>
          <button className="btn btn-info btn-sm mr-1" onClick={handleCreate}>
            <i className="fas fa-plus mr-1"></i> Criar Categoria
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
                    {/* checkbox geral para selecionar todos */}
                    <input
                      type="checkbox"
                      checked={
                        selectedIds.length === categories.length &&
                        categories.length > 0
                      }
                      onChange={(e) =>
                        setSelectedIds(
                          e.target.checked ? categories.map((c) => c.id) : []
                        )
                      }
                    />
                  </th>
                  <th>ID</th>
                  <th>Nome</th>
                  <th>Descrição</th>
                  <th>Criado em</th>
                  <th>Atualizado em</th>
                  <th className="text-center">Ações</th>
                </tr>
              </thead>
              <tbody>
                {categories.map((c) => (
                  <tr key={c.id}>
                    <td className="text-center">
                      <input
                        type="checkbox"
                        checked={selectedIds.includes(c.id)}
                        onChange={() => toggleSelect(c.id)}
                      />
                    </td>
                    <td>{c.id}</td>
                    <td>{c.name}</td>
                    <td>{c.description}</td>
                    <td>{formatDateTime(c.createdDate)}</td>
                    <td>{formatDateTime(c.updatedDate)}</td>
                    <td className="text-center" style={{ minWidth: 120 }}>
                      <button
                        className="btn btn-sm btn-info mr-1"
                        title="Visualizar"
                        onClick={() => navigate(`/categories/view/${c.id}`)}
                      >
                        <i className="fas fa-eye"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-warning mr-1"
                        title="Editar"
                        onClick={() => navigate(`/categories/update/${c.id}`)}
                      >
                        <i className="fas fa-edit"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-danger"
                        title="Deletar"
                        onClick={() => navigate(`/categories/delete/${c.id}`)}
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
