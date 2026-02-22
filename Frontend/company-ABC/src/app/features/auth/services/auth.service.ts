import { Injectable } from '@angular/core';
import { signal } from '@angular/core';
import { UserRole } from '../../../core/enums/roles.enum';
import { SessionStorageItems } from '../../../core/enums/session-storage.enum';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    currentUserRole = signal<UserRole>(UserRole.USER);
    currentUserEmail = signal<string>('');

    getUserRole(): UserRole {
        const role = sessionStorage.getItem(SessionStorageItems.ROL);
        return role ? (role as UserRole) : UserRole.USER;
    }

    logout(): void {
        sessionStorage.clear();
        this.currentUserRole.set(UserRole.USER);
        this.currentUserEmail.set('');
    }
    confirmAuthentication(): boolean {
        const token = sessionStorage.getItem(SessionStorageItems.TOKEN);
        return !!token;
    }
}
