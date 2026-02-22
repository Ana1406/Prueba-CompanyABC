import { Injectable } from '@angular/core';
import { EndPointRoute } from '../../../core/enums/end-point.enum';
import { enviroments } from '../../../../environments/enviroments';
import { HttpClient } from '@angular/common/http';
import { Payment } from '../model/payment.model';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private http: HttpClient) { }
  getPayments() {
    return this.http.get<Payment[]>(
      `${enviroments.API_PHOTOS}${EndPointRoute.GET_PAYMENTS}`
    );
  }
}
