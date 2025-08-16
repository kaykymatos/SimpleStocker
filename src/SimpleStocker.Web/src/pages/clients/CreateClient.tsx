import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Client } from '../../shared/models/Client'
import { ClientService } from '../../shared/services/ClientService'
import { useMemo } from 'react'

export default function CreateClient() {
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
      await clientService.create(form as Client)
      navigate('/clients/list')
    } catch {
      setError('Erro ao criar cliente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card shadow mb-4">
      <div className="card-header py-3">
        <h5 className="m-0 font-weight-bold text-primary">Criar Cliente</h5>
      </div>
      <div className="card-body">
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group col-md-6">
              <label>Nome</label>
              <input
                type="text"
                name="name"
                className="form-control"
                value={form.name}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-group col-md-6">
              <label>Email</label>
              <input
                type="email"
                name="email"
                className="form-control"
                value={form.email}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="form-row">
            <div className="form-group col-md-6">
              <label>Telefone</label>
              <input
                type="text"
                name="phoneNumer"
                className="form-control"
                value={form.phoneNumer}
                onChange={handleChange}
              />
            </div>
            <div className="form-group col-md-3">
              <label>Endereço</label>
              <input
                type="text"
                name="address"
                className="form-control"
                value={form.address}
                onChange={handleChange}
              />
            </div>
            <div className="form-group col-md-3">
              <label>Número</label>
              <input
                type="text"
                name="addressNumber"
                className="form-control"
                value={form.addressNumber}
                onChange={handleChange}
              />
            </div>
          </div>
          <div className="form-row">
            <div className="form-group col-md-4">
              <label>Data de Nascimento</label>
              <input
                type="date"
                name="birthDate"
                className="form-control"
                value={form.birthDate}
                onChange={handleChange}
              />
            </div>
            <div className="form-group col-md-2 d-flex align-items-center">
              <div className="form-check mt-4">
                <input
                  type="checkbox"
                  name="active"
                  className="form-check-input"
                  checked={form.active}
                  onChange={handleChange}
                  id="activeCheck"
                />
                <label className="form-check-label" htmlFor="activeCheck">
                  Ativo
                </label>
              </div>
            </div>
          </div>
          {error && <div className="alert alert-danger">{error}</div>}
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? 'Salvando...' : 'Salvar'}
          </button>
        </form>
      </div>
    </div>
  )
}
