import { ChangeDetectorRef, Component, inject, signal, WritableSignal } from '@angular/core';
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
import { ApiResponse } from '../../../../core/models/api-response.interface';
import { LoginInterface } from '../../models/login.model';
import { jwtDecode } from "jwt-decode";
import { ISession } from '../../../../core/models/ISession.interface';
import { ModalCreate } from '../../../users/pages/modal-create/modal-create';

@Component({
  selector: 'app-login',
  imports: [CustomInput, CustomButton, ReactiveFormsModule, CommonModule, ModalCreate],
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
  message = signal('');

  isModalOpen: WritableSignal<boolean> = signal(false);


  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, CustomValidators.password]),
  });

  private router: Router = inject(Router);
  private authService: AuthService = inject(AuthService);
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);


  async handleLogin(payload: LoginInterface) {
    this.isLoadingSignal.set(true);
    this.buttonTextSignal.set('Iniciando sesión...');

    this.authService.login(payload).subscribe({
      next: async (response: ApiResponse<string>) => {
        this.message.set(response.message);

        this.loginSuccessSignal.set(true);
        this.buttonTextSignal.set('Ingresar');
        await this.setToken(response.data);
        this.router.navigate([RoutesApp.HOME]);
        this.cdr.detectChanges();


      },
      error: (err: any) => {
        this.isLoadingSignal.set(false);
        this.buttonTextSignal.set('Ingresar');
        this.loginSuccessSignal.set(false);
        this.message.set(err.error.message || 'Error al iniciar sesión');
      },
    });
  }
  async setToken(token: string) {
    const decodedToken: ISession = jwtDecode(token);
    sessionStorage.setItem(SessionStorageItems.ROL, decodedToken.Rol);
    sessionStorage.setItem(SessionStorageItems.TOKEN, token);
    sessionStorage.setItem(SessionStorageItems.USEREMAIL, decodedToken.Email);
    sessionStorage.setItem(SessionStorageItems.NAME, decodedToken.Name);
    sessionStorage.setItem(SessionStorageItems.ID, decodedToken.Id.toString());
  }
  openModal() {
    this.isModalOpen.set(true);
  }
  closeModal() {
    this.isModalOpen.set(false);
  }
}
