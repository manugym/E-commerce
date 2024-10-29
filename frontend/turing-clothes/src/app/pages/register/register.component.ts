import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RegisterDto } from '../../models/register-dto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  Name: string;
  Surname: string;
  Email: string;
  Password: string;
  Address: string;

  constructor(private authService: AuthService) {}

  async submit() {
    const authData: RegisterDto = {
      Name: this.Name,
      Surname: this.Surname,
      Email: this.Email,
      Password: this.Password,
      Address: this.Address,
    };
    const result = await this.authService.register(authData);
  }
}
