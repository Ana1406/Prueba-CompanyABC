import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { enviroments } from '../../../../environments/enviroments';
import { EndPointRoute } from '../../../core/enums/end-point.enum';
import { CreateUser, FilterUser, Users } from '../models/users.model';
import { ApiResponse } from '../../../core/models/api-response.interface';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private httpClient: HttpClient = inject(HttpClient);

  getAllUsers(payload: FilterUser) {
    return this.httpClient.get<ApiResponse<Users[]>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.GET_ALL_USERS}?email=${payload.email}&page=${payload.page}&pageSize=${payload.pageSize}`
    );
  }
  createUser(payload: CreateUser) {
    return this.httpClient.post<ApiResponse<string>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.CREATE_USER}`, payload
    );
  }

}
