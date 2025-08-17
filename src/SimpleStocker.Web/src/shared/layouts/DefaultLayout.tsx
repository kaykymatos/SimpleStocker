import type { ReactNode } from 'react'
import { Link } from 'react-router-dom'
import { useCallback } from 'react'
import reactLogo from '../../assets/react.svg'

type LayoutProps = {
  children: ReactNode
}

export default function DefaultLayout({ children }: LayoutProps) {
  const userName = 'User Name'
  const handleSidebarToggle = useCallback(() => {
    document.body.classList.toggle('sidebar-toggled')
    const sidebar = document.getElementById('accordionSidebar')
    if (sidebar) {
      sidebar.classList.toggle('toggled')
      if (sidebar.classList.contains('toggled')) {
        const collapses = sidebar.querySelectorAll('.collapse.show')
        collapses.forEach((el) => {
          el.classList.remove('show')
        })
      }
    }
  }, [])

  return (
    <>
      <div id="wrapper">
        <ul
          className="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"
          id="accordionSidebar"
        >
          <li className="nav-item sidebar-brand d-flex align-items-center justify-content-center">
            <a href="index.html">
              <div className="sidebar-brand-icon rotate-n-15">
                <i className="fas fa-laugh-wink"></i>
              </div>
              <div className="sidebar-brand-text mx-3">SimpleStocker</div>
            </a>
          </li>
          <li>
            <hr className="sidebar-divider my-0" />
          </li>

          <li>
            <hr className="sidebar-divider" />
          </li>

          <div className="sidebar-heading">Interface</div>
          <li className="nav-item">
            <a
              className="nav-link collapsed"
              href="#"
              data-toggle="collapse"
              data-target="#collapseCategories"
              aria-expanded="true"
              aria-controls="collapseCategories"
            >
              <i className="fas fa-fw fa-tags"></i>
              <span>Categorias</span>
            </a>
            <div
              id="collapseCategories"
              className="collapse"
              aria-labelledby="headingCategories"
              data-parent="#accordionSidebar"
            >
              <div className="bg-white py-2 collapse-inner rounded">
                <div className="sidebar-heading">Categorias</div>
                <Link className="collapse-item" to="/categories/create">
                  Criar Categoria
                </Link>
                <Link className="collapse-item" to="/categories/list">
                  Listar Categorias
                </Link>
              </div>
            </div>
          </li>
          <li className="nav-item">
            <a
              className="nav-link collapsed"
              href="#"
              data-toggle="collapse"
              data-target="#collapseTwo"
              aria-expanded="true"
              aria-controls="collapseTwo"
            >
              <i className="fas fa-fw fa-box"></i>
              <span>Produtos</span>
            </a>
            <div
              id="collapseTwo"
              className="collapse"
              aria-labelledby="headingTwo"
              data-parent="#accordionSidebar"
            >
              <div className="bg-white py-2 collapse-inner rounded">
                <div className="sidebar-heading">Produtos</div>

                <Link className="collapse-item" to="/products/create">
                  Criar Produto
                </Link>
                <Link className="collapse-item" to="/products/list">
                  Listar Produtos
                </Link>
              </div>
            </div>
          </li>

          <li className="nav-item">
            <a
              className="nav-link collapsed"
              href="#"
              data-toggle="collapse"
              data-target="#collapseClients"
              aria-expanded="true"
              aria-controls="collapseClients"
            >
              <i className="fas fa-fw fa-user"></i>
              <span>Clientes</span>
            </a>
            <div
              id="collapseClients"
              className="collapse"
              aria-labelledby="headingClients"
              data-parent="#accordionSidebar"
            >
              <div className="bg-white py-2 collapse-inner rounded">
                <div className="sidebar-heading">Clientes</div>
                <Link className="collapse-item" to="/clients/create">
                  Criar Cliente
                </Link>
                <Link className="collapse-item" to="/clients/list">
                  Listar Clientes
                </Link>
              </div>
            </div>
          </li>

          <li className="nav-item">
            <a
              className="nav-link collapsed"
              href="#"
              data-toggle="collapse"
              data-target="#collapseSales"
              aria-expanded="true"
              aria-controls="collapseSales"
            >
              <i className="fas fa-fw fa-shopping-cart"></i>
              <span>Vendas</span>
            </a>
            <div
              id="collapseSales"
              className="collapse"
              aria-labelledby="headingSales"
              data-parent="#accordionSidebar"
            >
              <div className="bg-white py-2 collapse-inner rounded">
                <div className="sidebar-heading">Vendas</div>
                <Link className="collapse-item" to="/sales/create">
                  Criar Venda
                </Link>
                <Link className="collapse-item" to="/sales/list">
                  Listar Vendas
                </Link>
              </div>
            </div>
          </li>

          <li>
            <hr className="sidebar-divider" />
          </li>

          <li>
            <div className="text-center d-none d-md-inline">
              <button
                className="rounded-circle border-0"
                id="sidebarToggle"
                type="button"
                onClick={handleSidebarToggle}
              ></button>
            </div>
          </li>
        </ul>

        <div id="content-wrapper" className="d-flex flex-column">
          <div id="content">
            <nav className="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">
              <button
                id="sidebarToggleTop"
                className="btn btn-link d-md-none rounded-circle mr-3"
              >
                <i className="fa fa-bars"></i>
              </button>

              <form className="d-none d-sm-inline-block form-inline mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search">
                <div className="input-group">
                  <input
                    type="text"
                    className="form-control bg-light border-0 small"
                    placeholder="Search for..."
                    aria-label="Search"
                    aria-describedby="basic-addon2"
                  />
                  <div className="input-group-append">
                    <button className="btn btn-primary" type="button">
                      <i className="fas fa-search fa-sm"></i>
                    </button>
                  </div>
                </div>
              </form>

              <ul className="navbar-nav ml-auto">
                <li className="nav-item dropdown no-arrow d-sm-none">
                  <a
                    className="nav-link dropdown-toggle"
                    href="#"
                    id="searchDropdown"
                    role="button"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                    <i className="fas fa-search fa-fw"></i>
                  </a>
                  <div
                    className="dropdown-menu dropdown-menu-right p-3 shadow animated--grow-in"
                    aria-labelledby="searchDropdown"
                  >
                    <form className="form-inline mr-auto w-100 navbar-search">
                      <div className="input-group">
                        <input
                          type="text"
                          className="form-control bg-light border-0 small"
                          placeholder="Search for..."
                          aria-label="Search"
                          aria-describedby="basic-addon2"
                        />
                        <div className="input-group-append">
                          <button className="btn btn-primary" type="button">
                            <i className="fas fa-search fa-sm"></i>
                          </button>
                        </div>
                      </div>
                    </form>
                  </div>
                </li>

                <div className="topbar-divider d-none d-sm-block"></div>

                <li className="nav-item dropdown no-arrow">
                  <a
                    className="nav-link dropdown-toggle"
                    href="#"
                    id="userDropdown"
                    role="button"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                    <span className="mr-2 d-none d-lg-inline text-gray-600 small">
                      {userName}
                    </span>
                    <img
                      className="img-profile rounded-circle"
                      src={reactLogo}
                      alt="Avatar"
                    />
                  </a>

                  <div
                    className="dropdown-menu dropdown-menu-right shadow animated--grow-in"
                    aria-labelledby="userDropdown"
                  >
                    <a className="dropdown-item" href="#">
                      <i className="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>
                      Profile
                    </a>
                    <a className="dropdown-item" href="#">
                      <i className="fas fa-cogs fa-sm fa-fw mr-2 text-gray-400"></i>
                      Settings
                    </a>
                    <a className="dropdown-item" href="#">
                      <i className="fas fa-list fa-sm fa-fw mr-2 text-gray-400"></i>
                      Activity Log
                    </a>
                    <div className="dropdown-divider"></div>
                    <a
                      className="dropdown-item"
                      href="#"
                      data-toggle="modal"
                      data-target="#logoutModal"
                    >
                      <i className="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                      Logout
                    </a>
                  </div>
                </li>
              </ul>
            </nav>
            <div className="container-fluid">{children}</div>
          </div>
          <footer className="sticky-footer bg-white">
            <div className="container my-auto">
              <div className="copyright text-center my-auto">
                <span>Copyright &copy; Your Website 2020</span>
              </div>
            </div>
          </footer>
        </div>
      </div>
      <a className="scroll-to-top rounded" href="#page-top">
        <i className="fas fa-angle-up"></i>
      </a>

      <div
        className="modal fade"
        id="logoutModal"
        tabIndex={-1}
        role="dialog"
        aria-labelledby="exampleModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title" id="exampleModalLabel">
                Ready to Leave?
              </h5>
              <button
                className="close"
                type="button"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">×</span>
              </button>
            </div>
            <div className="modal-body">
              Select "Logout" below if you are ready to end your current
              session.
            </div>
            <div className="modal-footer">
              <button
                className="btn btn-secondary"
                type="button"
                data-dismiss="modal"
              >
                Cancel
              </button>
              <a className="btn btn-primary" href="login.html">
                Logout
              </a>
            </div>
          </div>
        </div>
      </div>
    </>
  )
}
