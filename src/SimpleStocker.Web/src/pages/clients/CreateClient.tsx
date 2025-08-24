import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Client } from '../../shared/models/Client'
import { ClientService } from '../../shared/services/ClientService'
import { useMemo } from 'react'
import { getApiFieldErrors } from '../../shared/utils/apiErrorFieldHelper'

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
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({})
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
    const now = new Date().toISOString()
    const clientDTO = {
      id: 0,
      createdDate: now,
      updatedDate: now,
      name: form.name ?? '',
      email: form.email ?? '',
      phoneNumer: form.phoneNumer ?? '',
      address: form.address ?? '',
      addressNumber: form.addressNumber ?? '',
      active: form.active ?? true,
      birthDate: form.birthDate ? new Date(form.birthDate).toISOString() : now,
    }
    try {
      await clientService.create(clientDTO)
      navigate('/clients/list')
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
                placeholder="Digite o nome do cliente"
              />
              {fieldErrors.Name && (
                <div className="text-danger small mt-1">{fieldErrors.Name}</div>
              )}
            </div>
            <div className="form-group col-md-6">
              <label>Email</label>
              <input
                type="email"
                name="email"
                className="form-control"
                value={form.email}
                onChange={handleChange}
                placeholder="Digite o e-mail"
              />
              {fieldErrors.Email && (
                <div className="text-danger small mt-1">
                  {fieldErrors.Email}
                </div>
              )}
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
                placeholder="Digite o telefone"
              />
              {fieldErrors.PhoneNumer && (
                <div className="text-danger small mt-1">
                  {fieldErrors.PhoneNumer}
                </div>
              )}
            </div>
            <div className="form-group col-md-3">
              <label>Endereço</label>
              <input
                type="text"
                name="address"
                className="form-control"
                value={form.address}
                onChange={handleChange}
                placeholder="Digite o endereço"
              />
              {fieldErrors.Address && (
                <div className="text-danger small mt-1">
                  {fieldErrors.Address}
                </div>
              )}
            </div>
            <div className="form-group col-md-3">
              <label>Número</label>
              <input
                type="text"
                name="addressNumber"
                className="form-control"
                value={form.addressNumber}
                onChange={handleChange}
                placeholder="Digite o número"
              />
              {fieldErrors.AddressNumber && (
                <div className="text-danger small mt-1">
                  {fieldErrors.AddressNumber}
                </div>
              )}
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
                placeholder="Selecione a data de nascimento"
              />
              {fieldErrors.BirthDate && (
                <div className="text-danger small mt-1">
                  {fieldErrors.BirthDate}
                </div>
              )}
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
