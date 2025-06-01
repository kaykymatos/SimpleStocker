import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ListProductsComponent } from './modules/product/pages/list-products/list-products.component';
import { CreateProductComponent } from './modules/product/pages/create-product/create-product.component';
import { UpdateProductComponent } from './modules/product/pages/update-product/update-product.component';
import { DetailsProductComponent } from './modules/product/pages/details-product/details-product.component';
import { DeleteProductComponent } from './modules/product/pages/delete-product/delete-product.component';
import { ListCategoriesComponent } from './modules/category/pages/list-categories/list-categories.component';
import { ListClientsComponent } from './modules/client/pages/list-clients/list-clients.component';
import { ListSalesComponent } from './modules/sale/pages/list-sales/list-sales.component';
import { CreateSaleComponent } from './modules/sale/pages/create-sale/create-sale.component';
import { UpdateSaleComponent } from './modules/sale/pages/update-sale/update-sale.component';
import { DetailsSaleComponent } from './modules/sale/pages/details-sale/details-sale.component';
import { DeleteSaleComponent } from './modules/sale/pages/delete-sale/delete-sale.component';
import { CreateClientComponent } from './modules/client/pages/create-client/create-client.component';
import { UpdateClientComponent } from './modules/client/pages/update-client/update-client.component';
import { DetailsClientComponent } from './modules/client/pages/details-client/details-client.component';
import { DeleteClientComponent } from './modules/client/pages/delete-client/delete-client.component';
import { CreateCategoryComponent } from './modules/category/pages/create-category/create-category.component';
import { UpdateCategoryComponent } from './modules/category/pages/update-category/update-category.component';
import { DetailsCategoryComponent } from './modules/category/pages/details-category/details-category.component';
import { DeleteCategoryComponent } from './modules/category/pages/delete-category/delete-category.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ListProductsComponent,
    CreateProductComponent,
    UpdateProductComponent,
    DetailsProductComponent,
    DeleteProductComponent,
    ListCategoriesComponent,
    ListClientsComponent,
    ListSalesComponent,
    CreateSaleComponent,
    UpdateSaleComponent,
    DetailsSaleComponent,
    DeleteSaleComponent,
    CreateClientComponent,
    UpdateClientComponent,
    DetailsClientComponent,
    DeleteClientComponent,
    CreateCategoryComponent,
    UpdateCategoryComponent,
    DetailsCategoryComponent,
    DeleteCategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
