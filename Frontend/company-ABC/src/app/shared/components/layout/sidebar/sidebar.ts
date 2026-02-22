import { Component, inject, OnInit, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SidebarService } from '../../../services/sidebar.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faBars, faTimes } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { CustomButton } from '../../forms-components/custom-button/custom-button';
import { RoutesApp } from '../../../../core/enums/routes.enum';

@Component({
    selector: 'app-sidebar',
    standalone: true,
    imports: [CommonModule, FontAwesomeModule, CustomButton, RouterModule],
    templateUrl: './sidebar.html',
    styleUrl: './sidebar.css'
})
export class SidebarComponent implements OnInit {
    sidebarService = inject(SidebarService);
    faBars = faBars;
    faTimes = faTimes;

    private authService = inject(AuthService);
    private router = inject(Router);
    ngOnInit(): void {
        this.checkWindowSize();
    }

    @HostListener('window:resize', ['$event'])
    onResize(event: any): void {
        this.checkWindowSize();
    }

    checkWindowSize(): void {
        if (window.innerWidth <= 768) {
            this.sidebarService.isOpen.set(false);
        } else {
            this.sidebarService.isOpen.set(true);
        }
    }

    get menuItems() {
        return this.sidebarService.getMenuItems();
    }

    get isSidebarOpen() {
        return this.sidebarService.isOpen();
    }

    toggleSidebar(): void {
        this.sidebarService.toggleSidebar();
    }
    logout() {
        this.authService.logout();
        this.router.navigate([RoutesApp.LOGIN]);

    }
}
