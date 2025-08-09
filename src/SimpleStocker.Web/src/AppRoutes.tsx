import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import DefaultLayout from "./shared/layouts/DefaultLayout";
import { Dashboard } from "./pages/home/Dashboard";
import CreateProduct from "./pages/products/CreateProduct";
import DeleteProduct from "./pages/products/DeleteProduct";
import ListProducts from "./pages/products/ListProducts";
import UpdateProduct from "./pages/products/UpdateProduct";
import ViewProduct from "./pages/products/ViewProduct";
import CreateClient from "./pages/clients/CreateClient";
import DeleteClient from "./pages/clients/DeleteClient";
import ListClients from "./pages/clients/ListClients";
import UpdateClient from "./pages/clients/UpdateClient";
import ViewClient from "./pages/clients/ViewClient";
import CreateSale from "./pages/sales/CreateSale";
import DeleteSale from "./pages/sales/DeleteSale";
import ListSales from "./pages/sales/ListSales";
import UpdateSale from "./pages/sales/UpdateSale";
import ViewSale from "./pages/sales/ViewSale";

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<DefaultLayout><Outlet /></DefaultLayout>}>
          <Route path="/" element={<Dashboard />} />

          {/* Produtos */}
          <Route path="/products/create" element={<CreateProduct />} />
          <Route path="/products/delete" element={<DeleteProduct />} />
          <Route path="/products/list" element={<ListProducts />} />
          <Route path="/products/update" element={<UpdateProduct />} />
          <Route path="/products/view" element={<ViewProduct />} />

          {/* Clientes */}
          <Route path="/clients/create" element={<CreateClient />} />
          <Route path="/clients/delete" element={<DeleteClient />} />
          <Route path="/clients/list" element={<ListClients />} />
          <Route path="/clients/update" element={<UpdateClient />} />
          <Route path="/clients/view" element={<ViewClient />} />

          {/* Vendas */}
          <Route path="/sales/create" element={<CreateSale />} />
          <Route path="/sales/delete" element={<DeleteSale />} />
          <Route path="/sales/list" element={<ListSales />} />
          <Route path="/sales/update" element={<UpdateSale />} />
          <Route path="/sales/view" element={<ViewSale />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
