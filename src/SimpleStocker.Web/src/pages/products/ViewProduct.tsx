import { useEffect, useMemo, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Product } from '../../shared/models/Product'
import { ProductService } from '../../shared/services/ProductService'
import { formatDateTime } from '../../shared/utils/dateUtils'

export default function ViewProduct() {
  const { id } = useParams()
  const productService = useMemo(() => new ProductService(), [])
  const [product, setProduct] = useState<Product | null>(null)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (id) {
      productService
        .getOne(id)
        .then((res) => setProduct(res.data ?? null))
        .finally(() => setLoading(false))
    }
  }, [id, productService])

  if (loading) return <div>Carregando...</div>
  if (!product) return <div>Produto não encontrado.</div>

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">
          Detalhes do Produto
        </h5>
      </div>
      <div className="card-body">
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
        <p>
          <strong>Estoque:</strong> {product.quantityStock}
        </p>
        <p>
          <strong>Unidade de Medida:</strong> {product.unityOfMeasurement}
        </p>
        <p>
          <strong>ID da Categoria:</strong> {product.categoryId}
        </p>
        <p>
          <strong>Nome da Categoria:</strong> {product.categoryName}
        </p>
        <p>
          <strong>Criado em:</strong> {formatDateTime(product.createdDate)}
        </p>
        <p>
          <strong>Atualizado em:</strong> {formatDateTime(product.updatedDate)}
        </p>
      </div>
    </div>
  )
}
