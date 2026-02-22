import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { RoutesApp } from '../enums/routes.enum';
import { AuthService } from '../../features/auth/services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthService);

  if (!authService.confirmAuthentication()) {
    const router = inject(Router);
    router.navigateByUrl(RoutesApp.LOGIN);
    return false;
  }

  return true;

};
