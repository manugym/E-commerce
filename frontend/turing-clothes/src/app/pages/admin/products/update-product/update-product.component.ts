import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../../../models/product-dto';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AdminService } from '../../../../services/admin.service';
import { FormsModule } from '@angular/forms';
import { SidebarComponent } from "../../../../shared/sidebar/sidebar.component";

@Component({
  selector: 'app-update-product',
  standalone: true,
  imports: [FormsModule, SidebarComponent, RouterLink],
  templateUrl: './update-product.component.html',
  styleUrl: './update-product.component.css'
})
export class UpdateProductComponent implements OnInit {
  product: ProductDto;
  productId: number;

  constructor(
    private route: ActivatedRoute,
    private adminService: AdminService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    this.productId = this.route.snapshot.queryParamMap.get(
      'productId'
    ) as unknown as number;
    const result = await this.adminService.getProductById(this.productId);
    if (result.success) {
      this.product = result.data;
    } else {
      alert('Error al cargar el producto');
      this.router.navigate(['/admin/products']);
    }
  }

  async updateProduct(): Promise<void> {
    const result = await this.adminService.updateProduct(this.product.id, this.product);
    if (result.success) {
      alert('Producto actualizado correctamente');
      this.router.navigate(['/admin/products']);
    } else {
      alert('Error al actualizar el producto');
    }
  }
}