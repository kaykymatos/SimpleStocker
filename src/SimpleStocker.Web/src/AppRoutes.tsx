import { BrowserRouter, Routes, Route, Outlet } from 'react-router-dom'
import DefaultLayout from './shared/layouts/DefaultLayout'
import { Dashboard } from './pages/home/Dashboard'
import CreateProduct from './pages/products/CreateProduct'
import DeleteProduct from './pages/products/DeleteProduct'
import ListProducts from './pages/products/ListProducts'
import UpdateProduct from './pages/products/UpdateProduct'
import ViewProduct from './pages/products/ViewProduct'
import CreateClient from './pages/clients/CreateClient'
import DeleteClient from './pages/clients/DeleteClient'
import ListClients from './pages/clients/ListClients'
import UpdateClient from './pages/clients/UpdateClient'
import ViewClient from './pages/clients/ViewClient'
import CreateSale from './pages/sales/CreateSale'
import DeleteSale from './pages/sales/DeleteSale'
import ListSales from './pages/sales/ListSales'
import UpdateSale from './pages/sales/UpdateSale'
import ViewSale from './pages/sales/ViewSale'
import CreateCategory from './pages/categories/CreateCategory'
import DeleteCategory from './pages/categories/DeleteCategory'
import UpdateCategory from './pages/categories/UpdateCategory'
import ViewCategory from './pages/categories/ViewCategory'
import ListCategories from './pages/categories/ListCategories'

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          element={
            <DefaultLayout>
              <Outlet />
            </DefaultLayout>
          }
        >
          <Route path="/" element={<Dashboard />} />

          {/* Categorias */}
          <Route path="/categories/create" element={<CreateCategory />} />
          <Route path="/categories/delete/:id" element={<DeleteCategory />} />
          <Route path="/categories/list" element={<ListCategories />} />
          <Route path="/categories/update/:id" element={<UpdateCategory />} />
          <Route path="/categories/view/:id" element={<ViewCategory />} />
          {/* Produtos */}
          <Route path="/products/create" element={<CreateProduct />} />
          <Route path="/products/delete/:id" element={<DeleteProduct />} />
          <Route path="/products/list" element={<ListProducts />} />
          <Route path="/products/update/:id" element={<UpdateProduct />} />
          <Route path="/products/view/:id" element={<ViewProduct />} />

          {/* Clientes */}
          <Route path="/clients/create" element={<CreateClient />} />
          <Route path="/clients/delete/:id" element={<DeleteClient />} />
          <Route path="/clients/list" element={<ListClients />} />
          <Route path="/clients/update/:id" element={<UpdateClient />} />
          <Route path="/clients/view/:id" element={<ViewClient />} />

          {/* Vendas */}
          <Route path="/sales/create" element={<CreateSale />} />
          <Route path="/sales/delete/:id" element={<DeleteSale />} />
          <Route path="/sales/list" element={<ListSales />} />
          <Route path="/sales/update/:id" element={<UpdateSale />} />
          <Route path="/sales/view/:id" element={<ViewSale />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}
