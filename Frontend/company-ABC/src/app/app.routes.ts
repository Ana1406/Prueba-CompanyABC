import { Routes } from '@angular/router';
import { RoutesApp } from './core/enums/routes.enum';
import { Login } from './features/auth/pages/login/login';
import { Home } from './features/home/home';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { UserRole } from './core/enums/roles.enum';
import { LayoutComponent } from './shared/components/layout/layout';

export const routes: Routes = [{
    path: RoutesApp.LOGIN,
    component: Login,
},
{
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
        {
            path: RoutesApp.HOME,
            loadComponent: () => import('./features/home/home').then(m => m.Home),
        }, {
            path: RoutesApp.ORDERS,
            loadComponent: () => import('./features/orders/pages/orders-table/orders-table').then(m => m.OrdersTable),
        }, {
            path: RoutesApp.USERS,
            loadComponent: () => import('./features/users/pages/user-table/user-table').then(m => m.UserTable),
            canActivate: [roleGuard],
            data: { roles: [UserRole.ADMIN] },
        }, {
            path: RoutesApp.PAYMENTS,
            loadComponent: () => import('./features/payments/pages/payments-table/payments-table').then(m => m.PaymentsTable),
        },
    ]
},
{ path: '', component: Login },
];
