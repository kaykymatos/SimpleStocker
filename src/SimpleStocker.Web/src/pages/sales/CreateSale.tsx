import { useState, useEffect, useMemo } from 'react'
import { useNavigate } from 'react-router-dom'
import { Product } from '../../shared/models/Product'
import { ProductService } from '../../shared/services/ProductService'
import { Client } from '../../shared/models/Client'
import { ClientService } from '../../shared/services/ClientService'
import { SaleService } from '../../shared/services/SaleService'
import { getApiFieldErrors } from '../../shared/utils/apiErrorFieldHelper'

export default function CreateSale() {
  const saleService = useMemo(() => new SaleService(), [])
  const clientService = useMemo(() => new ClientService(), [])
  const productService = useMemo(() => new ProductService(), [])
  const [clients, setClients] = useState<Client[]>([])
  const [products, setProducts] = useState<Product[]>([])
  const [form, setForm] = useState<{
    clientId: number
    items: Array<{ productId: number; quantity: number }>
    discount: number
    paymentMethod: number
  }>({
    clientId: 0,
    items: [{ productId: 0, quantity: 1 }],
    discount: 0,
    paymentMethod: 0,
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({})
  const navigate = useNavigate()

  useEffect(() => {
    clientService
      .getAll()
      .then((res) => setClients(res.data ?? []))
      .catch(() => setClients([]))
    productService
      .getAll()
      .then((res) => setProducts(res.data ?? []))
      .catch(() => setProducts([]))
  }, [clientService, productService])

  function handleChange(
    e: React.ChangeEvent<HTMLSelectElement | HTMLInputElement>
  ) {
    const { name, value, type } = e.target
    setForm((prev) => ({
      ...prev,
      [name]: type === 'number' ? Number(value) : value,
    }))
  }

  function handleItemChange(
    idx: number,
    field: 'productId' | 'quantity',
    value: string | number
  ) {
    const newItems = [...form.items]
    newItems[idx][field] = field === 'quantity' ? Number(value) : Number(value)
    setForm((prev) => ({ ...prev, items: newItems }))
  }

  function addItem() {
    setForm((prev) => ({
      ...prev,
      items: [...prev.items, { productId: 0, quantity: 1 }],
    }))
  }

  function removeItem(idx: number) {
    setForm((prev) => ({
      ...prev,
      items: prev.items.filter((_, i) => i !== idx),
    }))
  }

  function getSubTotal(item: { productId: number; quantity: number }) {
    const prod = products.find((p) => p.id === item.productId)
    return prod ? prod.price * item.quantity : 0
  }

  const totalAmount =
    form.items.reduce((sum, item) => sum + getSubTotal(item), 0) - form.discount
  const paymentMethods = [
    { value: 0, label: 'Dinheiro' },
    { value: 1, label: 'Cartão de crédito' },
    { value: 2, label: 'Cartão de débito' },
    { value: 3, label: 'Pix' },
    { value: 4, label: 'Transferência bancária' },
    { value: 5, label: 'Boleto bancário' },
    { value: 6, label: 'Carteira digital' },
    { value: 7, label: 'Moeda digital' },
  ]

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setLoading(true)
    setError(null)
    setFieldErrors({})
    const now = new Date().toISOString()
    const saleDTO = {
      id: 0,
      createdDate: now,
      updatedDate: now,
      items: form.items.map((item) => ({
        id: 0,
        createdDate: now,
        updatedDate: now,
        saleId: 0,
        productId: item.productId,
        quantity: item.quantity,
        unityPrice: products.find((p) => p.id === item.productId)?.price ?? 0,
        subTotal:
          (products.find((p) => p.id === item.productId)?.price ?? 0) *
          item.quantity,
      })),
      totalAmount:
        form.items.reduce(
          (sum, item) =>
            sum +
            (products.find((p) => p.id === item.productId)?.price ?? 0) *
              item.quantity,
          0
        ) - form.discount,
      clientId: form.clientId,
      discount: form.discount,
      paymentMethod: form.paymentMethod,
      status: 0,
    }
    try {
      await saleService.create(saleDTO)

      navigate('/sales/list')
    } catch (err: any) {
      const apiError = err?.response?.data
      setFieldErrors(getApiFieldErrors(apiError))
      if (apiError?.message) setError(apiError?.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Criar Venda</h5>
      </div>
      <div className="card-body">
        {error && <div className="alert alert-danger">{error}</div>}
        <form onSubmit={handleSubmit}>
          <div className="row">
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="clientId">Cliente</label>
                <select
                  id="clientId"
                  name="clientId"
                  value={form.clientId}
                  onChange={handleChange}
                  className="form-control"
                >
                  <option value={0} disabled>
                    Selecione o cliente
                  </option>
                  {clients.map((c) => (
                    <option key={c.id} value={c.id}>
                      {c.name}
                    </option>
                  ))}
                </select>
                {fieldErrors.ClientId && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.ClientId}
                  </div>
                )}
              </div>
            </div>
          </div>
          <h5>Itens da Venda</h5>
          {form.items.map((item, idx) => (
            <div className="row mb-2" key={idx}>
              <div className="col-md-6">
                <select
                  className="form-control"
                  value={item.productId}
                  onChange={(e) =>
                    handleItemChange(idx, 'productId', e.target.value)
                  }
                >
                  {fieldErrors[`items[${idx}].quantity`] && (
                    <div className="text-danger small mt-1">
                      {fieldErrors[`items[${idx}].quantity`]}
                    </div>
                  )}
                  <option value={0} disabled>
                    Selecione o produto
                  </option>
                  {products.map((p) => (
                    <option key={p.id} value={p.id}>
                      {p.name}
                    </option>
                  ))}
                </select>
              </div>
              <div className="col-md-3">
                <input
                  type="number"
                  className="form-control"
                  min={1}
                  value={item.quantity}
                  onChange={(e) =>
                    handleItemChange(idx, 'quantity', e.target.value)
                  }
                  placeholder="Quantidade"
                />
              </div>
              <div className="col-md-2">
                <input
                  type="text"
                  className="form-control"
                  value={getSubTotal(item).toFixed(2)}
                  readOnly
                  placeholder="Subtotal"
                />
              </div>
              <div className="col-md-1">
                {form.items.length > 1 && (
                  <button
                    type="button"
                    className="btn btn-danger"
                    onClick={() => removeItem(idx)}
                  >
                    -
                  </button>
                )}
              </div>
            </div>
          ))}
          <button
            type="button"
            className="btn btn-secondary mb-3"
            onClick={addItem}
          >
            Adicionar Produto
          </button>
          <div className="row">
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="discount">Desconto</label>
                <input
                  id="discount"
                  name="discount"
                  type="number"
                  min={0}
                  value={form.discount}
                  onChange={handleChange}
                  placeholder="Desconto em reais"
                  className="form-control"
                />
              </div>
            </div>
            <div className="col-md-6">
              <div className="form-group mb-3">
                <label htmlFor="paymentMethod">Método de Pagamento</label>
                <select
                  id="paymentMethod"
                  name="paymentMethod"
                  value={form.paymentMethod}
                  onChange={handleChange}
                  className="form-control"
                >
                  {paymentMethods.map((pm) => (
                    <option key={pm.value} value={pm.value}>
                      {pm.label}
                    </option>
                  ))}
                </select>
                {fieldErrors.paymentMethod && (
                  <div className="text-danger small mt-1">
                    {fieldErrors.paymentMethod}
                  </div>
                )}
              </div>
            </div>
          </div>
          <div className="form-group mb-3">
            <label>Total</label>
            <input
              type="text"
              className="form-control"
              value={totalAmount.toFixed(2)}
              readOnly
              placeholder="Total da venda"
            />
            {fieldErrors.TotalAmount && (
              <div className="text-danger small mt-1">
                {fieldErrors.TotalAmount}
              </div>
            )}
          </div>
          <button
            className="btn btn-primary mt-3"
            type="submit"
            disabled={loading}
          >
            Salvar Venda
          </button>
        </form>
      </div>
    </div>
  )
}
