import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Category } from '../../shared/models/Category'
import { CategoryService } from '../../shared/services/CategoryService'
import { useMemo } from 'react'

export default function ViewCategory() {
  const { id } = useParams()
  const categoryService = useMemo(() => new CategoryService(), [])
  const [category, setCategory] = useState<Category | null>(null)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (id) {
      categoryService
        .getOne(id)
        .then((res) => setCategory(res.data ?? null))
        .finally(() => setLoading(false))
    }
  }, [id, categoryService])

  if (loading) return <div>Carregando...</div>
  if (!category) return <div>Categoria não encontrada.</div>

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">
          Detalhes da Categoria
        </h5>
      </div>
      <div className="card-body">
        <p>
          <strong>ID:</strong> {category.id}
        </p>
        <p>
          <strong>Nome:</strong> {category.name}
        </p>
        <p>
          <strong>Descrição:</strong> {category.description}
        </p>
      </div>
    </div>
  )
}
