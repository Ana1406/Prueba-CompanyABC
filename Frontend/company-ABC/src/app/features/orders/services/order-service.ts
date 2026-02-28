import { Injectable } from '@angular/core';
import { enviroments } from '../../../../environments/enviroments';
import { EndPointRoute } from '../../../core/enums/end-point.enum';
import { HttpClient } from '@angular/common/http';
import { Order } from '../models/order.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) { }
  getOrders() {
    return this.http.get<Order[]>(
      `${enviroments.API_PUBLIC}${EndPointRoute.GET_ALL_ORDERS}`
    );
  }
}
