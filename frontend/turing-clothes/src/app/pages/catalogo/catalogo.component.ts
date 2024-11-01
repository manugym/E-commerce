import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { SmartSearchComponent } from '../smart-search/smart-search.component';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [HeaderComponent, SmartSearchComponent],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css',
})
export class CatalogoComponent {
  productList: ProductDto[] = [];
  imageUrl: string[];
  constructor(private catalogService: CatalogService) {}

  ngOnInit() {
    this.getAllProducts();
  }

  async getAllProducts() {
    const result = await this.catalogService.getAllProducts();

    if (result.success) {
      this.productList = result.data.map((product) => ({
        ...product,
        image: `https://localhost:7183/${product.image}`,
      }));
    }
  }
}
