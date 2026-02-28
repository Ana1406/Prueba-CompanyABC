import { inject, Injectable } from '@angular/core';
import { signal } from '@angular/core';
import { UserRole } from '../../../core/enums/roles.enum';
import { SessionStorageItems } from '../../../core/enums/session-storage.enum';
import { HttpClient } from '@angular/common/http';

import { EndPointRoute } from '../../../core/enums/end-point.enum';
import { enviroments } from '../../../../environments/enviroments';
import { LoginInterface } from '../models/login.model';
import { ApiResponse } from '../../../core/models/api-response.interface';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    currentUserRole = signal<UserRole>(UserRole.USER);
    currentUserEmail = signal<string>('');
    private httpClient: HttpClient = inject(HttpClient);


    login(payload: LoginInterface) {
        return this.httpClient.post<ApiResponse<string>>(
            `${enviroments.API_PUBLIC}${EndPointRoute.LOGIN}`, payload
        );
    }

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
