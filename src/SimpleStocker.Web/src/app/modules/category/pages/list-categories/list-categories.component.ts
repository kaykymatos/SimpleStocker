import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../../../../models/category.model';

@Component({
  selector: 'app-list-categories',
  standalone: false,
  templateUrl: './list-categories.component.html',
  styleUrl: './list-categories.component.css',
})
export class ListCategoriesComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  public categorys: Category[] = [
    {
      id: 1,
      name: 'Category 1',
      description: 'Description of Category 1',

      createdDate: new Date(),
      updatedDate: new Date(),
    },
    {
      id: 2,
      name: 'Category 2',
      description: 'Description of Category 2',

      createdDate: new Date(),
      updatedDate: new Date(),
    },
  ];
  filterText: string = '';

  get filteredListItens(): Category[] {
    if (!this.filterText) return this.categorys;
    const filter = this.filterText.toLowerCase();
    return this.categorys.filter(
      (p) =>
        p.name?.toLowerCase().includes(filter) ||
        p.description?.toLowerCase().includes(filter)
    );
  }
  onAdd() {
    this.router.navigate(['/categories/create']);
  }
  onEdit(category: Category) {
    this.router.navigate(['/categories/update', category.id]);
  }
  onDelete(category: Category) {
    this.router.navigate(['/categories/delete', category.id]);
  }
}
