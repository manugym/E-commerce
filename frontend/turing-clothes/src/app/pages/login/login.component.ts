import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AuthDto } from '../../models/auth-dto';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})



export class LoginComponent {
  email: string = "";
  password: string = "";
  jwt: string = ' ';


  constructor(private AuthService: AuthService) { }

  async submit() {
    const authData: AuthDto = { Email: this.email, Password: this.password };
    const result = await this.AuthService.login(authData);

    if (result.success) {
      this.jwt = result.data.accessToken;
      console.log(this.jwt)
    }
  }
}
