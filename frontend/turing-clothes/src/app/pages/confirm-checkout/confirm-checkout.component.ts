import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Order } from '../../models/order';
import { Subscription } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../models/user-dto';
import { CheckoutService } from '../../services/checkout.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-confirm-checkout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './confirm-checkout.component.html',
  styleUrls: ['./confirm-checkout.component.css'],
})
export class ConfirmCheckoutComponent implements OnInit {
  routeParamMap$: Subscription;
  // order: Order = {
  //   id: 0,
  //   userId: 0,
  //   paymentMethod: '',
  //   email: '',
  //   transactionStatus: '',
  //   totalPrice: 0,
  //   orderDetails: [],
  // };
  orderDto: UserDto;
  lastOrder: Order;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private checkoutService: CheckoutService,
  ) {}

  async ngOnInit(): Promise<void> {
    this.routeParamMap$ = this.route.paramMap.subscribe(async (paramMap) => {
      // const id = paramMap.get('id') as unknown as number;
      const result = await this.userService.getUserOrder();
      this.orderDto = result.data;
      this.lastOrder = this.orderDto.orders[this.orderDto.orders.length - 1];
      this.lastOrder.orderDetails.map((img) => {
        img.product.image = `${environment.imageUrl}${img.product.image}`;
      });
      if (this.lastOrder.email) {
        try {
          const user: User = await this.checkoutService.getUserByEmail(this.lastOrder.email);  
          this.sendOrderEmail(user); 
        } catch (error) {
          console.error('Error al obtener los datos del usuario:', error);
        }
      }
    });
  }

  sendOrderEmail(user: User) {
    const userName = user
      ? `${user.name} ${user.surname}`
      : 'Cliente'; 
    const paymentMethod = this.lastOrder.paymentMethod;
    const deliveryAddress = user?.address || "Dirección no especificada";
    const totalPrice = this.lastOrder.totalPrice / 100;
    const totalPriceInEth = paymentMethod === "Ethereum" ? (totalPrice / 1600).toFixed(4) : null;
    const productRows = this.lastOrder.orderDetails
      .map((detail) => {
        const imageUrl = detail.product.image.startsWith('http')
          ? detail.product.image
          : `https://localhost:7183/${detail.product.image}`;
        const unitPrice = (detail.product.price / 100).toFixed(2);
        const totalProductPrice = (
          (detail.product.price * detail.amount) /
          100
        ).toFixed(2);
        return `
          <tr>
            <td style="padding: 8px; border: 1px solid #ccc; text-align: center;">
              <img src="${imageUrl}" alt="${detail.product.name}" style="max-width: 50px; max-height: 50px;" />
            </td>
            <td style="padding: 8px; border: 1px solid #ccc; text-align: center;">${detail.product.name}</td>
            <td style="padding: 8px; border: 1px solid #ccc; text-align: center;">${detail.amount}</td>
            <td style="padding: 8px; border: 1px solid #ccc; text-align: center;">${unitPrice}€</td>
            <td style="padding: 8px; border: 1px solid #ccc; text-align: center;">${totalProductPrice}€</td>
          </tr>`;
      })
      .join('');
    const emailHtml = `
        <div style="font-family: Arial, sans-serif; color: #333;">
        <h2>Factura de tu compra en Turing Clothes</h2>
        <p>Hola <strong>${userName}</strong>,</p>
        <p>Gracias por tu compra. Aquí tienes el resumen de tu pedido:</p>
        <table style="width: 100%; border-collapse: collapse; margin-top: 20px;">
          <thead>
            <tr>
              <th style="padding: 8px; border: 1px solid #ccc; text-align: center;">Imagen</th>
              <th style="padding: 8px; border: 1px solid #ccc; text-align: center;">Nombre</th>
              <th style="padding: 8px; border: 1px solid #ccc; text-align: center;">Cantidad</th>
              <th style="padding: 8px; border: 1px solid #ccc; text-align: center;">Precio unitario</th>
              <th style="padding: 8px; border: 1px solid #ccc; text-align: center;">Precio total</th>
            </tr>
          </thead>
          <tbody>
            ${productRows}
          </tbody>
        </table>
        <p><strong>Método de pago:</strong> ${paymentMethod}</p>
        <p><strong>Total:</strong> ${totalPrice.toFixed(2)}€</p>
        ${
          totalPriceInEth
            ? `<p><strong>Total en Ethereum:</strong> ${totalPriceInEth} ETH</p>`
            : ''
        }
        <p><strong>Dirección de entrega:</strong> ${deliveryAddress}</p>
        <p>Gracias por confiar en Turing Clothes.</p>
      </div>
    `;
    this.checkoutService.sendEmail(this.lastOrder.email, emailHtml)
        .then(() => {
            console.log("Correo enviado con éxito.");
            alert("¡Correo enviado correctamente!");
        })
        .catch(error => {
            console.error("Error enviando el correo:", error);
            alert("Hubo un error al enviar el correo.");
        });
}
}
