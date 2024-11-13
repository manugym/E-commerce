import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {

  product: ProductDto | null = null;

  constructor(private activatedRoute: ActivatedRoute, private catalogService: CatalogService) {}

  async ngOnInit(): Promise<void> {
    const id = this.activatedRoute.snapshot.paramMap.get('id') as unknown as number;

    const result = await this.catalogService.getProductDetailsById(id);
    
    result.data.image = `https://localhost:7183/${result.data.image}`;
    this.product = result.data;
  }

}
