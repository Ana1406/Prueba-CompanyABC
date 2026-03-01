import { Injectable } from '@angular/core';
import { enviroments } from '../../../../environments/enviroments';
import { EndPointRoute } from '../../../core/enums/end-point.enum';
import { HttpClient } from '@angular/common/http';
import { CreateOrder, DeleteOrder, Order } from '../models/order.model';
import { ApiResponse } from '../../../core/models/api-response.interface';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) { }
  getOrders() {
    return this.http.get<ApiResponse<Order[]>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.GET_ALL_ORDERS}`
    );
  }
  CreateOrder(payload: CreateOrder) {
    return this.http.post<ApiResponse<string>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.CREATE_ORDER}`, payload
    );
  }

  EditOrder(payload: CreateOrder) {
    return this.http.put<ApiResponse<string>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.EDIT_ORDER}`, payload
    );
  }
  DeleteOrder(payload: DeleteOrder) {
    return this.http.post<ApiResponse<string>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.DELETE_ORDER}`, payload
    );
  }
}
