import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListCategoriesComponent } from './modules/category/pages/list-categories/list-categories.component';
import { ListClientsComponent } from './modules/client/pages/list-clients/list-clients.component';
import { CreateProductComponent } from './modules/product/pages/create-product/create-product.component';
import { DeleteProductComponent } from './modules/product/pages/delete-product/delete-product.component';
import { DetailsProductComponent } from './modules/product/pages/details-product/details-product.component';
import { ListProductsComponent } from './modules/product/pages/list-products/list-products.component';
import { UpdateProductComponent } from './modules/product/pages/update-product/update-product.component';
import { ListSalesComponent } from './modules/sale/pages/list-sales/list-sales.component';
import { CreateCategoryComponent } from './modules/category/pages/create-category/create-category.component';
import { DeleteCategoryComponent } from './modules/category/pages/delete-category/delete-category.component';
import { DetailsCategoryComponent } from './modules/category/pages/details-category/details-category.component';
import { UpdateCategoryComponent } from './modules/category/pages/update-category/update-category.component';
import { CreateClientComponent } from './modules/client/pages/create-client/create-client.component';
import { DeleteClientComponent } from './modules/client/pages/delete-client/delete-client.component';
import { DetailsClientComponent } from './modules/client/pages/details-client/details-client.component';
import { UpdateClientComponent } from './modules/client/pages/update-client/update-client.component';
import { CreateSaleComponent } from './modules/sale/pages/create-sale/create-sale.component';
import { DeleteSaleComponent } from './modules/sale/pages/delete-sale/delete-sale.component';
import { DetailsSaleComponent } from './modules/sale/pages/details-sale/details-sale.component';
import { UpdateSaleComponent } from './modules/sale/pages/update-sale/update-sale.component';

const routes: Routes = [
  { path: 'products', component: ListProductsComponent },
  { path: 'products/create', component: CreateProductComponent },
  { path: 'products/update/:id', component: UpdateProductComponent },
  { path: 'products/details/:id', component: DetailsProductComponent },
  { path: 'products/delete/:id', component: DeleteProductComponent },

  { path: 'categories', component: ListCategoriesComponent },
  { path: 'categories/create', component: CreateCategoryComponent },
  { path: 'categories/update/:id', component: UpdateCategoryComponent },
  { path: 'categories/details/:id', component: DetailsCategoryComponent },
  { path: 'categories/delete/:id', component: DeleteCategoryComponent },

  { path: 'clients', component: ListClientsComponent },
  { path: 'clients/create', component: CreateClientComponent },
  { path: 'clients/update/:id', component: UpdateClientComponent },
  { path: 'clients/details/:id', component: DetailsClientComponent },
  { path: 'clients/delete/:id', component: DeleteClientComponent },

  { path: 'sales', component: ListSalesComponent },
  { path: 'sales/create', component: CreateSaleComponent },
  { path: 'sales/update/:id', component: UpdateSaleComponent },
  { path: 'sales/details/:id', component: DetailsSaleComponent },
  { path: 'sales/delete/:id', component: DeleteSaleComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
