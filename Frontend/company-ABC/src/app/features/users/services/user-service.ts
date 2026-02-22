import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { enviroments } from '../../../../environments/enviroments';
import { EndPointRoute } from '../../../core/enums/end-point.enum';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) { }
  getUsers() {
    return this.http.get<User[]>(
      `${enviroments.API_PUBLIC}${EndPointRoute.GET_USERS}`
    );
  }
}
