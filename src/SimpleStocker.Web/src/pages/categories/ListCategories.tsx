import { useEffect, useState } from 'react'
import { formatDateTime } from '../../shared/utils/dateUtils'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'
import { useNavigate } from 'react-router-dom'
import { useMemo } from 'react'

export default function ListCategories() {
  const categoryService = useMemo(() => new CategoryService(), [])
  const [categories, setCategories] = useState<Category[]>([])
  const [loading, setLoading] = useState(true)
  const navigate = useNavigate()

  useEffect(() => {
    categoryService
      .getAll()
      .then((res) => setCategories(res.data ?? []))
      .finally(() => setLoading(false))
  }, [categoryService])

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h6 className="m-0 font-weight-bold text-primary">Listar Categorias</h6>
      </div>
      <div className="card-body">
        {loading ? (
          <div>Carregando...</div>
        ) : (
          <div className="table-responsive" style={{ maxHeight: 400 }}>
            <table className="table table-bordered table-hover table-sm mb-0">
              <thead className="thead-light">
                <tr>
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
