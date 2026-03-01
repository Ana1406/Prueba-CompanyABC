import { ChangeDetectorRef, Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../../services/payment-service';
import { Payment } from '../../model/payment.model';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';

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

  selectedPaymentSignal: WritableSignal<Payment | null> = signal(null);
  isModalOpen: WritableSignal<boolean> = signal(false);
  modalVariant: WritableSignal<'order' | 'payment'> = signal('payment');

  ngOnInit(): void {
    this.getPayments();
  }

  getPayments() {
    this.paymentService.getPayments().subscribe({
      next: (response: Payment[]) => {
        this.paymentsList = response;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading payments ', err);
        this.paymentsList = [];
      }
    });
  }

  openImage(payment: Payment) {
    this.selectedPaymentSignal.set(payment);
    this.modalVariant.set('payment');
    this.isModalOpen.set(true);
  }

  closeModal() {
    this.isModalOpen.set(false);
    this.selectedPaymentSignal.set(null);
  }

}
