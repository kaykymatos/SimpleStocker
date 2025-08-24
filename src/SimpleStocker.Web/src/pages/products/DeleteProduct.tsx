import { useNavigate, useParams } from 'react-router-dom'
import { useMemo, useState, useEffect } from 'react'
import { ProductService } from '../../shared/services/ProductService'
import { Product } from '../../shared/models/Product'

export default function DeleteProduct() {
  const [product, setProduct] = useState<Product | null>(null)
  const { id } = useParams()
  const productService = useMemo(() => new ProductService(), [])
  const navigate = useNavigate()
  useEffect(() => {
    if (id) {
      productService.getOne(id).then((res) => setProduct(res.data ?? null))
    }
  }, [id, productService])
  const handleDelete = async () => {
    if (!id) return
    try {
      await productService.delete(id)
      navigate('/products/list')
    } catch {
      alert('Erro ao deletar produto.')
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Deletar Produto</h5>
      </div>
      <div className="card-body">
        {product ? (
          <>
            <p>
              <strong>ID:</strong> {product.id}
            </p>
            <p>
              <strong>Nome:</strong> {product.name}
            </p>
            <p>
              <strong>Descrição:</strong> {product.description}
            </p>
            <p>
              <strong>Preço:</strong> {product.price}
            </p>
          </>
        ) : (
          <p>Carregando informações...</p>
        )}
        <p>Tem certeza que deseja deletar este produto?</p>
        <button className="btn btn-danger mr-2" onClick={handleDelete}>
          Deletar
        </button>
        <button
          className="btn btn-secondary"
          onClick={() => navigate('/products/list')}
        >
          Cancelar
        </button>
      </div>
    </div>
  )
}
