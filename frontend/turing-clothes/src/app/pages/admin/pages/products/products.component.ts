import { Component } from '@angular/core';
import { SidebarComponent } from '../../../../shared/sidebar/sidebar.component';
import { ProductDto } from '../../../../models/product-dto';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [SidebarComponent],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent {
  products: ProductDto[] = [
    {
      id: 1,
      name: 'product 1',
      description: 'description 1',
      price: 100,
      stock: 10,
      image: 'image1',
      reviews: [],
    },
    {
      id: 2,
      name: 'product 2',
      description: 'description 2',
      price: 200,
      stock: 20,
      image: 'image2',
      reviews: [],
    },
    {
      id: 3,
      name: 'product 3',
      description: 'description 3',
      price: 300,
      stock: 30,
      image: 'image3',
      reviews: [],
    },
  ];
}
