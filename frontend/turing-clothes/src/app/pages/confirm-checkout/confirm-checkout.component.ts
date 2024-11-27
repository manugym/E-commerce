import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-confirm-checkout',
  standalone: true,
  imports: [],
  templateUrl: './confirm-checkout.component.html',
  styleUrl: './confirm-checkout.component.css'
})
export class ConfirmCheckoutComponent implements OnInit {
  orderItems = [
    {
      name: 'Camiseta',
      quantity: 2,
      image: 'https://localhost:7183/images/Camiseta-cuello-redondo-black.png',
    },
    {
      name: 'Pantalón',
      quantity: 1,
      image: 'https://localhost:7183/images/pantalon-vaquero-hombre.png',
    },
  ];

  constructor() {}

  ngOnInit(): void {
  }
}
