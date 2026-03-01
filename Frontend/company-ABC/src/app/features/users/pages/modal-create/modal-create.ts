import { Component, Input, EventEmitter, Output, Signal, computed, inject } from '@angular/core';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';
import { CustomInput } from '../../../../shared/components/forms-components/custom-input/custom-input';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomValidators } from '../../../../core/validators/custom-validators';
import { CreateUser } from '../../models/users.model';
import { UserService } from '../../services/user-service';
import { CustomSelect } from '../../../../shared/components/forms-components/custom-select/custom-select';
import { Items } from '../../../../core/models/catalog.interface';

@Component({
  selector: 'app-modal-create',
  standalone: true,
  imports: [CustomButton, CustomInput, ReactiveFormsModule, CustomSelect],
  templateUrl: './modal-create.html',
  styleUrls: ['./modal-create.css'],
})
export class ModalCreate {
  options: Items[] = [
    { value: 'admin', text: 'Admin' },
    { value: 'user', text: 'User' }
  ];
  @Input() isOpen!: Signal<boolean>;
  @Output() close = new EventEmitter<void>();
  private userService: UserService = inject(UserService);

  createForm = new FormGroup({
    name: new FormControl(null, Validators.required),
    emailIn: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required, CustomValidators.password]),
    rol: new FormControl(null, Validators.required)

  });

  createUser(createUser: FormGroup) {
    if (this.createForm.invalid) return;
    this.userService.createUser(createUser.value).subscribe({
      next: (response) => {
        this.closeModal();
        this.createForm.reset();
      },
      error: (error) => {
      }
    });
  }

  closeModal() {
    this.close.emit();
  }
}
