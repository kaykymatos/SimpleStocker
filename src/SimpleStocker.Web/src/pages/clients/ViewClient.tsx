import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Client } from '../../shared/models/Client'
import { ClientService } from '../../shared/services/ClientService'
import { useMemo } from 'react'

export default function ViewClient() {
  const { id } = useParams()
  const clientService = useMemo(() => new ClientService(), [])
  const [client, setClient] = useState<Client | null>(null)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (id) {
      clientService
        .getOne(id)
        .then((res) => setClient(res.data ?? null))
        .finally(() => setLoading(false))
    }
  }, [id, clientService])

  if (loading) return <div>Carregando...</div>
  if (!client) return <div>Cliente não encontrado.</div>

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">
          Detalhes do Cliente
        </h5>
      </div>
      <div className="card-body">
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
        <p>
          <strong>Endereço:</strong> {client.address}
        </p>
        <p>
          <strong>Número:</strong> {client.addressNumber}
        </p>
        <p>
          <strong>Data de Nascimento:</strong> {client.birthDate}
        </p>
        <p>
          <strong>Ativo:</strong> {client.active ? 'Sim' : 'Não'}
        </p>
      </div>
    </div>
  )
}
