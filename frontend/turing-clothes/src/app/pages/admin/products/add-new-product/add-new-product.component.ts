import { Component } from '@angular/core';
import { ProductDto } from '../../../../models/product-dto';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../../../services/admin.service';

@Component({
  selector: 'app-add-new-product',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-new-product.component.html',
  styleUrl: './add-new-product.component.css',
})
export class AddNewProductComponent {
  product: ProductDto = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    stock: 0,
    image: '',
    reviews: [],
  };
  constructor(private adminService: AdminService) {

  }
  
  async addProduct(product: ProductDto) {
    product.price = product.price * 100;
    const result = await this.adminService.addProduct(product);
    if (result.success) {
      alert('Producto añadido correctamente');
    }
  }
}
