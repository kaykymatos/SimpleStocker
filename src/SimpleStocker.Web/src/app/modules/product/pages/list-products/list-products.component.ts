import { Component, OnInit } from '@angular/core';
import { Product } from '../../../../models/product.model';
import { FormsModule, NgModel } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-products',
  standalone: false,
  templateUrl: './list-products.component.html',
  styleUrl: './list-products.component.css',
})
export class ListProductsComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  public products: Product[] = [
    {
      id: 1,
      name: 'Product 1',
      description: 'Description of Product 1',
      quantityStock: 10,
      unityOfMeasurement: 'kg',
      price: 100,
      categoryId: 1,
      categoryName: 'Category 1',
      createdDate: new Date(),
      updatedDate: new Date(),
    },
    {
      id: 2,
      name: 'Product 2',
      description: 'Description of Product 2',
      quantityStock: 20,
      unityOfMeasurement: 'kg',
      price: 200,
      categoryId: 2,
      categoryName: 'Category 2',
      createdDate: new Date(),
      updatedDate: new Date(),
    },
  ];
  filterText: string = '';

  get filteredListItens(): Product[] {
    if (!this.filterText) return this.products;
    const filter = this.filterText.toLowerCase();
    return this.products.filter(
      (p) =>
        p.name?.toLowerCase().includes(filter) ||
        p.description?.toLowerCase().includes(filter) ||
        p.categoryName?.toLowerCase().includes(filter)
    );
  }
  onAdd() {
    this.router.navigate(['/products/create']);
  }
  onEdit(product: Product) {
    this.router.navigate(['/products/update', product.id]);
  }
  onDelete(product: Product) {
    this.router.navigate(['/products/delete', product.id]);
  }
}
