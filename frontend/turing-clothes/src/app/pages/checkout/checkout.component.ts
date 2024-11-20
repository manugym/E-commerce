import { Component } from '@angular/core';
import { StripeService } from 'ngx-stripe';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
    // Lista de productos en el carrito
    cartItems = [
      { id: 1, name: 'Camiseta', quantity: 2, price: 19.99, image: 'https://localhost:7183/images/Camiseta-cuello-redondo-black.png' },
      { id:2, name: 'Pantalón', quantity: 1, price: 39.99, image: 'https://localhost:7183/images/pantalon-vaquero-hombre.png' },
      { id:3, name: 'Chaqueta', quantity: 1, price: 59.99, image: 'https://localhost:7183/images/cazadoraAnte.png' }
    ];

    constructor(private stripe: StripeService) {}
  
    
    getTotal(): number {
      return this.cartItems.reduce(
        (total, item) => total + item.price * item.quantity,
        0
      );
    }
  
    proceedToPayment(): void {
      alert('Redirigiendo a la pasarela de pago...');
    }
}
