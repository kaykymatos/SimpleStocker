import { useNavigate, useParams } from 'react-router-dom'
import { SaleService } from '../../shared/services/SaleService'
import { useMemo, useState, useEffect } from 'react'
import { Sale } from '../../shared/models/Sale'

export default function DeleteSale() {
  const { id } = useParams()
  const saleService = useMemo(() => new SaleService(), [])
  const navigate = useNavigate()
  const [sale, setSale] = useState<Sale | null>(null)
  useEffect(() => {
    if (id) {
      saleService.getOne(id).then((res) => setSale(res.data ?? null))
    }
  }, [id, saleService])

  async function handleDelete() {
    if (!id) return
    try {
      await saleService.delete(id)
      navigate('/sales/list')
    } catch {
      alert('Erro ao deletar venda.')
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Deletar Venda</h5>
      </div>
      <div className="card-body">
        {sale ? (
          <>
            <p>
              <strong>ID:</strong> {sale.id}
            </p>
            <p>
              <strong>Cliente ID:</strong> {sale.clientId}
            </p>
            <p>
              <strong>Data de criação:</strong> {sale.createdDate}
            </p>
            <p>
              <strong>Data de atualização:</strong> {sale.updatedDate}
            </p>
            <p>
              <strong>Total:</strong> {sale.totalAmount}
            </p>
            <p>
              <strong>Desconto:</strong> {sale.discount}
            </p>
            <p>
              <strong>Método de pagamento:</strong> {sale.paymentMethod}
            </p>
            <p>
              <strong>Status:</strong> {sale.status}
            </p>
            <h6 className="mt-3">Itens da Venda:</h6>
            {sale.items && sale.items.length > 0 ? (
              <table className="table table-bordered table-sm">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Produto</th>
                    <th>Quantidade</th>
                    <th>Preço Unitário</th>
                    <th>Total</th>
                  </tr>
                </thead>
                <tbody>
                  {sale.items.map((item, idx) => (
                    <tr key={idx}>
                      <td>{item.id}</td>
                      <td>{item.productId ?? item.productId}</td>
                      <td>{item.quantity}</td>
                      <td>{item.unityPrice}</td>
                      <td>{item.subTotal}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            ) : (
              <p>Nenhum item encontrado.</p>
            )}
          </>
        ) : (
          <p>Carregando informações...</p>
        )}
        <p>Tem certeza que deseja deletar esta venda?</p>
        <button className="btn btn-danger mr-2" onClick={handleDelete}>
          Deletar
        </button>
        <button
          className="btn btn-secondary"
          onClick={() => navigate('/sales/list')}
        >
          Cancelar
        </button>
      </div>
    </div>
  )
}
