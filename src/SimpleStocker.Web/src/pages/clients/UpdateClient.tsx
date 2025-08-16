import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Client } from '../../shared/models/Client'
import { ClientService } from '../../shared/services/ClientService'
import { useMemo } from 'react'

export default function UpdateClient() {
  const { id } = useParams()
  const clientService = useMemo(() => new ClientService(), [])
  const [form, setForm] = useState<Partial<Client>>({
    name: '',
    email: '',
    phoneNumer: '',
    address: '',
    addressNumber: '',
    active: true,
    birthDate: '',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    if (id) {
      clientService
        .getOne(id)
        .then((res) =>
          setForm(
            res.data ?? {
              name: '',
              email: '',
              phoneNumer: '',
              address: '',
              addressNumber: '',
              active: true,
              birthDate: '',
            }
          )
        )
        .catch(() => setError('Cliente não encontrado.'))
    }
  }, [id, clientService])

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value, type, checked } = e.target
    setForm((prev) => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value,
    }))
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      await clientService.update(id!, form as Client)
      navigate('/clients/list')
    } catch {
      setError('Erro ao atualizar cliente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Atualizar Cliente</h5>
      </div>
      <div className="card-body">
        {error && <div className="alert alert-danger">{error}</div>}
        <form onSubmit={handleSubmit}>
          <input
            name="name"
            value={form.name ?? ''}
            onChange={handleChange}
            placeholder="Nome"
            className="form-control mb-2"
          />
          <input
            name="email"
            value={form.email ?? ''}
            onChange={handleChange}
            placeholder="Email"
            className="form-control mb-2"
          />
          <input
            name="phoneNumer"
            value={form.phoneNumer ?? ''}
            onChange={handleChange}
            placeholder="Telefone"
            className="form-control mb-2"
          />
          <input
            name="address"
            value={form.address ?? ''}
            onChange={handleChange}
            placeholder="Endereço"
            className="form-control mb-2"
          />
          <input
            name="addressNumber"
            value={form.addressNumber ?? ''}
            onChange={handleChange}
            placeholder="Número"
            className="form-control mb-2"
          />
          <input
            name="birthDate"
            value={form.birthDate ?? ''}
            onChange={handleChange}
            placeholder="Data de Nascimento"
            className="form-control mb-2"
          />
          <label className="form-check-label mr-2">
            <input
              type="checkbox"
              name="active"
              checked={form.active ?? true}
              onChange={handleChange}
              className="form-check-input"
            />{' '}
            Ativo
          </label>
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
