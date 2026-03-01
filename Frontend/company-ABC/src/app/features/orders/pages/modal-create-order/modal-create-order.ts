import { ChangeDetectorRef, Component, EventEmitter, inject, Input, OnChanges, OnInit, Output, signal, Signal, SimpleChanges } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { Items } from '../../../../core/models/catalog.interface';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';
import { CustomInput } from '../../../../shared/components/forms-components/custom-input/custom-input';
import { SessionStorageItems } from '../../../../core/enums/session-storage.enum';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../services/order-service';
import { Order } from '../../models/order.model';
import { ApiResponse } from '../../../../core/models/api-response.interface';

@Component({
  selector: 'app-modal-create-order',
  imports: [CustomButton, CustomInput, ReactiveFormsModule, CommonModule],
  templateUrl: './modal-create-order.html',
  styleUrl: './modal-create-order.css',
})
export class ModalCreateOrder implements OnInit, OnChanges {
  options: Items[] = [
    { value: 'admin', text: 'Admin' },
    { value: 'user', text: 'User' }
  ];
  @Input() isOpen!: Signal<boolean>;
  @Input() order: Order | null = null;
  @Output() close = new EventEmitter<void>();
  message = signal('');
  private orderService: OrderService = inject(OrderService);
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);
  createForm = new FormGroup({
    nameApplicant: new FormControl('', Validators.required),
    emailApplicant: new FormControl('', [Validators.required, Validators.email]),
    products: new FormArray([]),
    idUser: new FormControl('', Validators.required),
    idOrder: new FormControl('')

  });
  ngOnInit(): void {
    const idUserSession = sessionStorage.getItem(SessionStorageItems.ID) || '';
    const nameUserSession = sessionStorage.getItem(SessionStorageItems.NAME) || '';
    const emailUserSession = sessionStorage.getItem(SessionStorageItems.USEREMAIL) || '';

    this.createForm.patchValue({
      idUser: idUserSession,
      nameApplicant: nameUserSession,
      emailApplicant: emailUserSession
    });
    this.addProduct('');

  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes['order'] && this.order) {
      // Limpiar y agregar los productos del pedido
      this.products.clear();

      this.createForm.patchValue({
        nameApplicant: this.order.nameApplicant,
        emailApplicant: this.order.emailApplicant,
        idUser: this.order.idUser,
        idOrder: this.order.idOrder ?? ''
      });

      this.order.products.forEach(product => this.addProduct(product));
    } else if (changes['order'] && this.order === null) {
      // Crear nuevo pedido
      this.products.clear();
      this.addProduct('');
    }
  }
  get products(): FormArray {
    return this.createForm.get('products') as FormArray;
  }
  addProduct(initialValue: string = '') {
    this.products.push(new FormControl(initialValue, Validators.required));
  }

  removeProduct(index: number) {
    this.products.removeAt(index);
  }

  createOrder(createOrder: FormGroup) {
    if (this.createForm.invalid) return;
    this.orderService.CreateOrder(createOrder.value).subscribe({
      next: (response: ApiResponse<string>) => {
        if (response.status === 200) {
          this.closeModal();
          this.createForm.reset();
        } else {
          this.message.set(response.message);
        }
      },
      error: (error) => {
        this.message.set(error.error.message);
      }
    });
  }
  editOrder(createOrder: FormGroup) {
    if (this.createForm.invalid) return;
    this.orderService.EditOrder(createOrder.value).subscribe({
      next: (response: ApiResponse<string>) => {
        if (response.status === 200) {
          this.closeModal();
          this.createForm.reset();
        } else {
          this.message.set(response.message);
        }

      },
      error: (error) => {
        this.message.set(error.error.message);
      }
    });
  }

  closeModal() {
    this.close.emit();
    this.message.set('');

  }
}
