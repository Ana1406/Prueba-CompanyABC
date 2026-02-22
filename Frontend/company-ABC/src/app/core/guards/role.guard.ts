import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { RoutesApp } from '../enums/routes.enum';
import { AuthService } from '../../features/auth/services/auth.service';
import { UserRole } from '../enums/roles.enum';

export const roleGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const allowed = route.data && (route.data['roles'] as UserRole[] | undefined);

    const userRole = authService.getUserRole();

    if (allowed && allowed.length && !allowed.includes(userRole)) {
        router.navigateByUrl(RoutesApp.HOME);
        return false;
    }

    return true;
};
