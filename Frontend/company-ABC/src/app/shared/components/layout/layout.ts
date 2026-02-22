import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from './sidebar/sidebar';
import { SidebarService } from '../../services/sidebar.service';
import { AuthService } from '../../../features/auth/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-layout',
    standalone: true,
    imports: [RouterModule, SidebarComponent, CommonModule],
    templateUrl: './layout.html',
    styleUrl: './layout.css'
})
export class LayoutComponent implements OnInit {
    private sidebarService = inject(SidebarService);
    private authService = inject(AuthService);

    ngOnInit(): void {
        const userRole = this.authService.getUserRole();
        this.sidebarService.setUserRole(userRole);
    }
}

