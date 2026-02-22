import { ChangeDetectorRef, Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Order } from '../../models/order.model';
import { OrderService } from '../../services/order-service';
import { ModalComment } from '../../../../shared/components/modal-comment/modal-comment';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';

@Component({
  selector: 'app-orders-table',
  imports: [CommonModule, ModalComment, CustomButton],
  templateUrl: './orders-table.html',
  styleUrl: './orders-table.css',
})
export class OrdersTable implements OnInit {
  orders: Order[] = [];
  selectedOrderSignal: WritableSignal<Order | null> = signal(null);
  isModalOpen: WritableSignal<boolean> = signal(false);
  modalVariant: WritableSignal<'order' | 'payment'> = signal('order');

  private orderService = inject(OrderService);
  private cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrders().subscribe({
      next: (response: Order[]) => {
        this.orders = response,
          this.orders = response;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading orders', err);
        this.orders = [];
      }
    });
  }

  openComment(order: Order) {
    this.selectedOrderSignal.set(order);
    this.modalVariant.set('order');
    this.isModalOpen.set(true);
  }

  closeModal() {
    this.isModalOpen.set(false);
    this.selectedOrderSignal.set(null);
  }
}
