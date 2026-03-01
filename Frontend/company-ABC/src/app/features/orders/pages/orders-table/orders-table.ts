import { ChangeDetectorRef, Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteOrder, Order } from '../../models/order.model';
import { OrderService } from '../../services/order-service';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';
import { ApiResponse } from '../../../../core/models/api-response.interface';
import { ModalCreateOrder } from '../modal-create-order/modal-create-order';

@Component({
  selector: 'app-orders-table',
  imports: [CommonModule, CustomButton, ModalCreateOrder],
  templateUrl: './orders-table.html',
  styleUrl: './orders-table.css',
})
export class OrdersTable implements OnInit {
  orders: Order[] = [];
  orderSelect: Order | null = null;
  isModalOpen: WritableSignal<boolean> = signal(false);
  message = signal('');
  private orderService = inject(OrderService);
  private cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrders().subscribe({
      next: (response: ApiResponse<Order[]>) => {
        this.orders = response.data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading orders', err);
        this.orders = [];
      }
    });
  }
  deleteOrder(idOrder: string) {
    const payload: DeleteOrder = { idOrder: idOrder }
    this.orderService.DeleteOrder(payload).subscribe({
      next: (response: ApiResponse<string>) => {
        if (response.status === 200) {
          this.getOrders();
        } else {
          this.message.set(response.message);
        }

      },
      error: (error) => {
        this.message.set(error.error.message);
      }
    });
  }

  openModal(order?: Order) {
    if (order) {
      this.orderSelect = order;
    }
    console.log('Selected order for editing:', this.orderSelect);
    this.isModalOpen.set(true);
  }

  closeModal() {
    this.isModalOpen.set(false);
    this.orderSelect = null as any;
    this.getOrders();
  }
}
