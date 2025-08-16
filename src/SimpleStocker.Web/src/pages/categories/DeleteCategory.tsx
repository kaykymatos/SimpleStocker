import { useNavigate, useParams } from 'react-router-dom'
import { CategoryService } from '../../shared/services/CategoryService'
import { useMemo, useState, useEffect } from 'react'
import { Category } from '../../shared/models/Category'

export default function DeleteCategory() {
  const { id } = useParams()
  const categoryService = useMemo(() => new CategoryService(), [])
  const navigate = useNavigate()
  const [category, setCategory] = useState<Category | null>(null)
  useEffect(() => {
    if (id) {
      categoryService.getOne(id).then((res) => setCategory(res.data ?? null))
    }
  }, [id, categoryService])

  async function handleDelete() {
    if (!id) return
    try {
      await categoryService.delete(id)
      navigate('/categories/list')
    } catch {
      alert('Erro ao deletar categoria.')
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Deletar Categoria</h5>
      </div>
      <div className="card-body">
        {category ? (
          <>
            <p>
              <strong>ID:</strong> {category.id}
            </p>
            <p>
              <strong>Nome:</strong> {category.name}
            </p>
            <p>
              <strong>Descrição:</strong> {category.description}
            </p>
          </>
        ) : (
          <p>Carregando informações...</p>
        )}
        <p>Tem certeza que deseja deletar esta categoria?</p>
        <button className="btn btn-danger mr-2" onClick={handleDelete}>
          Deletar
        </button>
        <button
          className="btn btn-secondary"
          onClick={() => navigate('/categories/list')}
        >
          Cancelar
        </button>
      </div>
    </div>
  )
}
