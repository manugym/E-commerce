import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RegisterDto } from '../../models/register-dto';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {

  myForm: FormGroup;

  constructor(private authService: AuthService, private formBuilder: FormBuilder) {

    this.myForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      address: ['', Validators.required]
    },
    { validators: this.passwordMatchValidator });

   }

   // Validador de contraseña
  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPasswordControl = form.get('confirmPassword');
    const confirmPassword = confirmPasswordControl?.value;
    
    if (password !== confirmPassword) {
      confirmPasswordControl.setErrors({ mismatch: true });
    }
  }

  async submit() {
    const authData: RegisterDto = {
      name: this.myForm.get('name').toString(),
      surname: this.myForm.get('surname').toString(),
      email: this.myForm.get('email').toString(),
      password: this.myForm.get('password').toString(),
      address: this.myForm.get('address').toString(),
    };
    
    if (this.myForm.valid) {
      alert('Usuario registrado correctamente')
      const result = await this.authService.register(authData);
    } else {
      // El formulario no es válido
      alert('Formulario no válido');
    }
      
    
  }
}
