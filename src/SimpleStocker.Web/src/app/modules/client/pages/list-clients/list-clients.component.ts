import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../../../../models/category.model';
import { Client } from '../../../../models/client.model';

@Component({
  selector: 'app-list-clients',
  standalone: false,
  templateUrl: './list-clients.component.html',
  styleUrl: './list-clients.component.css',
})
export class ListClientsComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  public categorys: Client[] = [
    {
      id: 1,
      name: 'Cliente 1',
      email: 'cliente1@gmail.com',
      phoneNumber: '(11) 11111-1111',
      address: 'Rua 1',
      addressNumber: '122',
      active: true,
      birthDate: new Date(),
      createdDate: new Date(),
      updatedDate: new Date(),
    },
    {
      id: 2,
      name: 'Cliente 2',
      email: 'cliente2@gmail.com',
      phoneNumber: '(11) 22222-2222111',
      address: 'Rua 2',
      addressNumber: '22',
      active: true,
      birthDate: new Date(),
      createdDate: new Date(),
      updatedDate: new Date(),
    },
  ];
  filterText: string = '';

  get filteredListItens(): Client[] {
    if (!this.filterText) return this.categorys;
    const filter = this.filterText.toLowerCase();
    return this.categorys.filter((p) => p.name?.toLowerCase().includes(filter));
  }
  onAdd() {
    this.router.navigate(['/clients/create']);
  }
  onEdit(client: Client) {
    this.router.navigate(['/clients/update', client.id]);
  }
  onDelete(client: Client) {
    this.router.navigate(['/clients/delete', client.id]);
  }
}
