import { Component, Input, Output, EventEmitter, Signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Order } from '../../../features/orders/models/order.model';
import { Payment } from '../../../features/payments/model/payment.model';
import { CustomButton } from '../forms-components/custom-button/custom-button';

@Component({
  selector: 'app-modal-comment',
  imports: [CommonModule, CustomButton],
  templateUrl: './modal-comment.html',
  styleUrl: './modal-comment.css',
})
export class ModalComment {
  @Input() isOpen!: Signal<boolean>;
  @Input() payload!: Signal<Order | Payment | null>;
  @Input() variant!: Signal<'order' | 'payment'>;
  @Output() close = new EventEmitter<void>();

  orderPayload = computed(() => {
    try {
      return this.variant && this.variant() === 'order' ? (this.payload ? this.payload() as Order | null : null) : null;
    } catch {
      return null;
    }
  });

  paymentPayload = computed(() => {
    try {
      return this.variant && this.variant() === 'payment' ? (this.payload ? this.payload() as Payment | null : null) : null;
    } catch {
      return null;
    }
  });

  closeModal() {
    this.close.emit();
  }
}
