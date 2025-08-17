import { useEffect, useState, useMemo } from 'react'
import { formatDateTime } from '../../shared/utils/dateUtils'
import { Client } from '../../shared/models/Client'
import { ClientService } from '../../shared/services/ClientService'
import { useNavigate } from 'react-router-dom'

export default function ListClients() {
  const clientService = useMemo(() => new ClientService(), [])
  const [clients, setClients] = useState<Client[]>([])
  const [loading, setLoading] = useState(true)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const navigate = useNavigate()

  useEffect(() => {
    loadClients()
  }, [clientService])

  const loadClients = () => {
    setLoading(true)
    clientService
      .getAll()
      .then((res) => setClients(res.data ?? []))
      .finally(() => setLoading(false))
  }

  const toggleSelect = (id: number) => {
    setSelectedIds((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    )
  }

  const handleDeleteMany = async () => {
    if (selectedIds.length === 0) return

    if (
      !window.confirm(
        `Tem certeza que deseja deletar ${selectedIds.length} cliente(s)?`
      )
    ) {
      return
    }

    setLoading(true)
    try {
      await clientService.deleteMany(selectedIds)
      setTimeout(() => {
        setLoading(false)
        loadClients()
        setSelectedIds([])
      }, 1000)
    } catch (err: any) {
      setLoading(false)
      const apiError = err?.response?.data
      alert(apiError?.message || 'Erro ao deletar clientes')
    }
  }

  const handleCreate = () => {
    navigate('/clients/create')
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3 d-flex justify-content-between align-items-center">
        <h6 className="m-0 font-weight-bold text-primary">Listar Clientes</h6>
        <div>
          <button className="btn btn-info btn-sm mr-1" onClick={handleCreate}>
            <i className="fas fa-plus mr-1"></i> Criar Cliente
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
                    {/* checkbox geral para selecionar todos */}
                    <input
                      type="checkbox"
                      checked={
                        selectedIds.length === clients.length &&
                        clients.length > 0
                      }
                      onChange={(e) =>
                        setSelectedIds(
                          e.target.checked ? clients.map((c) => c.id) : []
                        )
                      }
                    />
                  </th>
                  <th>ID</th>
                  <th>Nome</th>
                  <th>Email</th>
                  <th>Telefone</th>
                  <th>Endereço</th>
                  <th>Número</th>
                  <th>Ativo</th>
                  <th>Nascimento</th>
                  <th className="text-center">Ações</th>
                </tr>
              </thead>
              <tbody>
                {clients.map((c) => (
                  <tr key={c.id}>
                    <td className="text-center">
                      <input
                        type="checkbox"
                        checked={selectedIds.includes(c.id)}
                        onChange={() => toggleSelect(c.id)}
                      />
                    </td>
                    <td>{c.id}</td>
                    <td>{c.name}</td>
                    <td>{c.email}</td>
                    <td>{c.phoneNumer}</td>
                    <td>{c.address}</td>
                    <td>{c.addressNumber}</td>
                    <td>{c.active ? 'Sim' : 'Não'}</td>
                    <td>{formatDateTime(c.birthDate)}</td>
                    <td className="text-center" style={{ minWidth: 120 }}>
                      <button
                        className="btn btn-sm btn-info mr-1"
                        title="Visualizar"
                        onClick={() => navigate(`/clients/view/${c.id}`)}
                      >
                        <i className="fas fa-eye"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-warning mr-1"
                        title="Editar"
                        onClick={() => navigate(`/clients/update/${c.id}`)}
                      >
                        <i className="fas fa-edit"></i>
                      </button>
                      <button
                        className="btn btn-sm btn-danger"
                        title="Deletar"
                        onClick={() => navigate(`/clients/delete/${c.id}`)}
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
