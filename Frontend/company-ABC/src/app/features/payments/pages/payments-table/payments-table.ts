import { ChangeDetectorRef, Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../../services/payment-service';
import { Payment } from '../../model/payment.model';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';
import { ApiResponse } from '../../../../core/models/api-response.interface';

@Component({
  selector: 'app-payments-table',
  imports: [CommonModule, CustomButton],
  templateUrl: './payments-table.html',
  styleUrl: './payments-table.css',
})
export class PaymentsTable implements OnInit {
  paymentsList: Payment[] = [];

  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);
  private paymentService = inject(PaymentService);
  message = signal('');
  selectedPaymentSignal: WritableSignal<Payment | null> = signal(null);
  isModalOpen: WritableSignal<boolean> = signal(false);
  modalVariant: WritableSignal<'order' | 'payment'> = signal('payment');

  ngOnInit(): void {
    this.getPayments();
  }

  getPayments() {
    this.paymentService.getPayments().subscribe({
      next: (response: ApiResponse<Payment[]>) => {
        this.paymentsList = response.data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading payments ', err);
        this.paymentsList = [];
      }
    });
  }

  updatePayment(idPayment: string) {
    setTimeout(() => {
      this.message.set('Realizando pago...');
    }, 2000);
    this.paymentService.updatePayment(idPayment).subscribe({
      next: (response: ApiResponse<Payment[]>) => {

        setTimeout(() => {
          this.message.set('Pago realizado con éxito');
        }, 2000);
        this.getPayments();
        this.message.set('');
      },
      error: (error) => {
        this.message.set(error.error.message);
      }
    });
  }
}

