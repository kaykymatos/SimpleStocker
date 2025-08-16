import { useNavigate, useParams } from 'react-router-dom'
import { ClientService } from '../../shared/services/ClientService'
import { useMemo, useState, useEffect } from 'react'
import { Client } from '../../shared/models/Client'

export default function DeleteClient() {
  const { id } = useParams()
  const clientService = useMemo(() => new ClientService(), [])
  const navigate = useNavigate()
  const [client, setClient] = useState<Client | null>(null)
  useEffect(() => {
    if (id) {
      clientService.getOne(id).then((res) => setClient(res.data ?? null))
    }
  }, [id, clientService])
  const handleDelete = async () => {
    if (!id) return
    try {
      await clientService.delete(id)
      navigate('/clients/list')
    } catch {
      alert('Erro ao deletar cliente.')
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Deletar Cliente</h5>
      </div>
      <div className="card-body">
        {client ? (
          <>
            <p>
              <strong>ID:</strong> {client.id}
            </p>
            <p>
              <strong>Nome:</strong> {client.name}
            </p>
            <p>
              <strong>Email:</strong> {client.email}
            </p>
            <p>
              <strong>Telefone:</strong> {client.phoneNumer}
            </p>
          </>
        ) : (
          <p>Carregando informações...</p>
        )}
        <p>Tem certeza que deseja deletar este cliente?</p>
        <button className="btn btn-danger mr-2" onClick={handleDelete}>
          Deletar
        </button>
        <button
          className="btn btn-secondary"
          onClick={() => navigate('/clients/list')}
        >
          Cancelar
        </button>
      </div>
    </div>
  )
}
