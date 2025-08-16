import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Sale } from '../../shared/models/Sale'
import { SaleService } from '../../shared/services/SaleService'
import { useMemo } from 'react'

export default function UpdateSale() {
  const { id } = useParams()
  const saleService = useMemo(() => new SaleService(), [])
  const [form, setForm] = useState<Partial<Sale>>({
    clientId: 0,
    totalAmount: 0,
    discount: 0,
    paymentMethod: '',
    status: '',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    if (id) {
      saleService
        .getOne(id)
        .then((res) =>
          setForm(
            res.data ?? {
              clientId: 0,
              totalAmount: 0,
              discount: 0,
              paymentMethod: '',
              status: '',
            }
          )
        )
        .catch(() => setError('Venda não encontrada.'))
    }
  }, [id, saleService])

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      await saleService.update(id!, form as Sale)
      navigate('/sales/list')
    } catch {
      setError('Erro ao atualizar venda.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Atualizar Venda</h5>
      </div>
      <div className="card-body">
        {error && <div className="alert alert-danger">{error}</div>}
        <form onSubmit={handleSubmit}>
          <input
            name="clientId"
            type="number"
            value={form.clientId ?? 0}
            onChange={handleChange}
            placeholder="ID do Cliente"
            className="form-control mb-2"
          />
          <input
            name="totalAmount"
            type="number"
            value={form.totalAmount ?? 0}
            onChange={handleChange}
            placeholder="Valor Total"
            className="form-control mb-2"
          />
          <input
            name="discount"
            type="number"
            value={form.discount ?? 0}
            onChange={handleChange}
            placeholder="Desconto"
            className="form-control mb-2"
          />
          <input
            name="paymentMethod"
            value={form.paymentMethod ?? ''}
            onChange={handleChange}
            placeholder="Forma de Pagamento"
            className="form-control mb-2"
          />
          <input
            name="status"
            value={form.status ?? ''}
            onChange={handleChange}
            placeholder="Status"
            className="form-control mb-2"
          />
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
