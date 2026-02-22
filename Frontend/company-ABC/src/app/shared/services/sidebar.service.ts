import { Injectable } from '@angular/core';
import { signal } from '@angular/core';
import { UserRole } from '../../core/enums/roles.enum';
import { RoutesApp } from '../../core/enums/routes.enum';
import { routes } from '../../app.routes';

export interface MenuItem {
  label: string;
  icon: string;
  route: string;
  roles: UserRole[];
  children?: MenuItem[];
}

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  userRole = signal<UserRole>(UserRole.USER);
  isOpen = signal(true);

  private menuItems: MenuItem[] = [
    {
      label: 'Dashboard',
      icon: 'fa-solid fa-chart-line',
      route: RoutesApp.HOME,
      roles: [UserRole.ADMIN, UserRole.USER]
    },
    {
      label: 'Usuarios',
      icon: 'fa-solid fa-users',
      route: RoutesApp.USERS,
      roles: [UserRole.ADMIN]
    },
    {
      label: 'Órdenes',
      icon: 'fa-solid fa-shopping-cart',
      route: RoutesApp.ORDERS,
      roles: [UserRole.ADMIN, UserRole.USER]
    },
    {
      label: 'Pagos',
      icon: 'fa-solid fa-credit-card',
      route: RoutesApp.PAYMENTS,
      roles: [UserRole.ADMIN, UserRole.USER]
    },
  ];

  getMenuItems(): MenuItem[] {
    return this.menuItems.filter(item =>
      item.roles.includes(this.userRole())
    );
  }

  toggleSidebar(): void {
    this.isOpen.set(!this.isOpen());
  }

  setUserRole(role: UserRole): void {
    this.userRole.set(role);
  }
}
