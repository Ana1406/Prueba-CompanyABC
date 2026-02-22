import { ChangeDetectorRef, Component, inject, signal } from '@angular/core';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';
import { CustomInput } from '../../../../shared/components/forms-components/custom-input/custom-input';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutesApp } from '../../../../core/enums/routes.enum';
import { CommonModule } from '@angular/common';
import { CustomValidators } from '../../../../core/validators/custom-validators';
import { SessionStorageItems } from '../../../../core/enums/session-storage.enum';
import { AuthService } from './../../services/auth.service';
import { UserRole } from '../../../../core/enums/roles.enum';

@Component({
  selector: 'app-login',
  imports: [CustomInput, CustomButton, ReactiveFormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  emailLabelSignal = 'Correo Electrónico';
  emailTypeSignal = 'email';
  passwordLabelSignal = 'Contraseña';
  passwordTypeSignal = 'password';

  buttonTextSignal = signal('Ingresar');
  isLoadingSignal = signal(false);
  loginSuccessSignal = signal(false);

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, CustomValidators.password]),
  });

  private router: Router = inject(Router);
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);


  handleLogin() {
    this.isLoadingSignal.set(true);
    this.buttonTextSignal.set('Iniciando sesión...');
    this.loginSuccessSignal.set(true);


    setTimeout(() => {
      this.isLoadingSignal.set(false);
      this.buttonTextSignal.set('Ingresar');
      this.loginSuccessSignal.set(false);
      this.setToken();
      this.router.navigate([RoutesApp.HOME]);
    }, 1000);
    this.cdr.detectChanges();


  }
  setToken() {
    const roles = Object.values(UserRole);
    const randomRole = roles[Math.floor(Math.random() * roles.length)] as UserRole;
    sessionStorage.setItem(SessionStorageItems.ROL, randomRole);
    sessionStorage.setItem(SessionStorageItems.TOKEN, 'fake-jwt-token');
    sessionStorage.setItem(SessionStorageItems.USEREMAIL, this.loginForm.get('email')?.value || '');
  }
}
