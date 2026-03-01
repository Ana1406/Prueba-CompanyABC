import { Injectable } from '@angular/core';
import { EndPointRoute } from '../../../core/enums/end-point.enum';
import { enviroments } from '../../../../environments/enviroments';
import { HttpClient } from '@angular/common/http';
import { Payment } from '../model/payment.model';
import { ApiResponse } from '../../../core/models/api-response.interface';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private http: HttpClient) { }
  getPayments() {
    return this.http.get<ApiResponse<Payment[]>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.GET_ALL_PAYMENTS}`
    );
  }
  updatePayment(idPayment: string) {
    return this.http.put<ApiResponse<Payment[]>>(
      `${enviroments.API_PUBLIC}${EndPointRoute.UPDATE_PAYMENT}?idPayment=${idPayment}`, null
    );
  }
}
