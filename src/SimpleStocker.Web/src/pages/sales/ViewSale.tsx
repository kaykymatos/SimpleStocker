import { useEffect, useMemo, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Sale } from '../../shared/models/Sale'
import { SaleService } from '../../shared/services/SaleService'

export default function ViewSale() {
  const { id } = useParams()
  const saleService = useMemo(() => new SaleService(), [])
  const [sale, setSale] = useState<Sale | null>(null)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (id) {
      saleService
        .getOne(id)
        .then((res) => setSale(res.data ?? null))
        .finally(() => setLoading(false))
    }
  }, [id, saleService])

  if (loading) return <div>Carregando...</div>
  if (!sale) return <div>Venda não encontrada.</div>

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Detalhes da Venda</h5>
      </div>
      <div className="card-body">
        <p>
          <strong>ID:</strong> {sale.id}
        </p>
        <p>
          <strong>ID do Cliente:</strong> {sale.clientId}
        </p>
        <p>
          <strong>Valor Total:</strong> {sale.totalAmount}
        </p>
        <p>
          <strong>Desconto:</strong> {sale.discount}
        </p>
        <p>
          <strong>Forma de Pagamento:</strong> {sale.paymentMethod}
        </p>
        <p>
          <strong>Status:</strong> {sale.status}
        </p>
      </div>
    </div>
  )
}
