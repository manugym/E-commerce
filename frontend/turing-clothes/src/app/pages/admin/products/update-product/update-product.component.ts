import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../../../models/product-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../../../../services/admin.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-product',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-product.component.html',
  styleUrl: './update-product.component.css'
})
export class UpdateProductComponent implements OnInit {
  product: ProductDto = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    stock: 0,
    image: '',
    reviews: [],
  };

  constructor(
    private route: ActivatedRoute,
    private adminService: AdminService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.product.id = this.route.snapshot.queryParamMap.get(
      'productId'
    ) as unknown as number;
  }

  async loadProduct(productId: number) {
    const result = await this.adminService.getProductById(productId);
    if (result.success) {
      this.product = result.data;
    } else {
      alert('Error al cargar el producto');
      this.router.navigate(['/admin/products']);
    }
  }

  async updateProduct() {
    const result = await this.adminService.updateProduct(this.product.id, this.product);
    if (result.success) {
      alert('Producto actualizado correctamente');
      this.router.navigate(['/admin/products']);
    } else {
      alert('Error al actualizar el producto');
    }
  }
}