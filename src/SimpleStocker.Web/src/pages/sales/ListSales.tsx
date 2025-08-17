import { useEffect, useState, useMemo } from 'react'
import { Sale } from '../../shared/models/Sale'
import { SaleService } from '../../shared/services/SaleService'
import { useNavigate } from 'react-router-dom'
import { formatDateTime } from '../../shared/utils/dateUtils'

export default function ListSales() {
  const saleService = useMemo(() => new SaleService(), [])
  const [sales, setSales] = useState<Sale[]>([])
  const [loading, setLoading] = useState(true)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const navigate = useNavigate()

  useEffect(() => {
    loadSales()
  }, [saleService])

  const loadSales = () => {
    setLoading(true)
    saleService
      .getAll()
      .then((res) => setSales(res.data ?? []))
      .finally(() => setLoading(false))
  }

  const toggleSelect = (id: number) => {
    setSelectedIds((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    )
  }
  const handleCreate = () => {
    navigate('/sales/create')
  }
  const handleDeleteMany = async () => {
    if (selectedIds.length === 0) return

    if (
      !window.confirm(
        `Tem certeza que deseja deletar ${selectedIds.length} venda(s)?`
      )
    ) {
      return
    }

    setLoading(true)
    try {
      await saleService.deleteMany(selectedIds)
      setTimeout(() => {
        setLoading(false)
        loadSales()
        setSelectedIds([])
      }, 1000)
    } catch (err: any) {
      setLoading(false)
      const apiError = err?.response?.data
      alert(apiError.message || 'Erro ao deletar vendas')
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3 d-flex justify-content-between align-items-center">
        <h6 className="m-0 font-weight-bold text-primary">Listar Vendas</h6>
        <div>
          <button className="btn btn-info btn-sm mr-1" onClick={handleCreate}>
            <i className="fas fa-plus mr-1"></i> Criar Venda
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
                    <input
                      type="checkbox"
                      checked={
                        selectedIds.length === sales.length && sales.length > 0
                      }
                      onChange={(e) =>
                        setSelectedIds(
                          e.target.checked ? sales.map((s) => s.id) : []
                        )
                      }
                    />
                  </th>
                  <th>ID</th>
                  <th>Data</th>
                  <th>Cliente</th>
                  <th>Total</th>
                  <th>Desconto</th>
                  <th>Pagamento</th>
                  <th>Status</th>
                  <th className="text-center">Ações</th>
                </tr>
              </thead>
              <tbody>
                {sales.map((s) => (
                  <tr key={s.id}>
                    <td className="text-center">
                      <input
                        type="checkbox"
                        checked={selectedIds.includes(s.id)}
                        onChange={() => toggleSelect(s.id)}
                      />
                    </td>
                    <td>{s.id}</td>
                    <td>{formatDateTime(s.createdDate)}</td>
                    <td>{s.clientId}</td>
                    <td>
                      {s.totalAmount.toLocaleString('pt-BR', {
                        style: 'currency',
                        currency: 'BRL',
                      })}
                    </td>
                    <td>
                      {s.discount.toLocaleString('pt-BR', {
                        style: 'currency',
                        currency: 'BRL',
                      })}
                    </td>
                    <td>{s.paymentMethod}</td>
                    <td>{s.status}</td>
                    <td className="text-center" style={{ minWidth: 120 }}>
                      <button
                        className="btn btn-sm btn-info mr-1"
                        title="Visualizar"
                        onClick={() => navigate(`/sales/view/${s.id}`)}
                      >
                        <i className="fas fa-eye"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-warning mr-1"
                        title="Editar"
                        onClick={() => navigate(`/sales/update/${s.id}`)}
                      >
                        <i className="fas fa-edit"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-danger"
                        title="Deletar"
                        onClick={() => navigate(`/sales/delete/${s.id}`)}
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
