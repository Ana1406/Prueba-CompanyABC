import { ChangeDetectorRef, Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user-service';
import { PhonePipe } from '../../../../core/pipes/phone-pipe';
import { FilterUser, Users } from '../../models/users.model';
import { ApiResponse } from '../../../../core/models/api-response.interface';
import { CustomButton } from '../../../../shared/components/forms-components/custom-button/custom-button';
import { CustomInput } from '../../../../shared/components/forms-components/custom-input/custom-input';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Paginator } from '../../../../shared/components/paginator/paginator';
import { ModalCreate } from '../modal-create/modal-create';

@Component({
  selector: 'app-user-table',
  imports: [CommonModule, PhonePipe, CustomInput, CustomButton, ReactiveFormsModule, Paginator, ModalCreate],
  templateUrl: './user-table.html',
  styleUrl: './user-table.css',
})
export class UserTable implements OnInit {
  userList: Users[] = [];
  currentPage: number = 1;
  pageSize: number = 10;
  totalRecords: number = 0;
  totalPages: number = 0;
  isModalOpen: WritableSignal<boolean> = signal(false);

  private userService: UserService = inject(UserService);
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);
  filterForm = new FormGroup({
    email: new FormControl('')
  })
  ngOnInit() {
    this.getUsers(this.filterForm.get('email')?.value || '', this.currentPage, this.pageSize);

  }
  getUsers(email: string, page: number, pageSize: number) {
    const payload: FilterUser = {
      email: email,
      page: page,
      pageSize: pageSize
    }
    this.userService.getAllUsers(payload).subscribe({
      next: (response: ApiResponse<Users[]>) => {
        this.userList = response.data;
        this.totalRecords = response.data.length;

        this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        this.cdr.detectChanges()
      },
      error: (error) => {
      }
    });
  }
  cleanFilters() {
    this.filterForm.reset();
    this.getUsers('', this.currentPage, this.pageSize);
  }
  openModal() {
    this.isModalOpen.set(true);
  }
  closeModal() {
    this.isModalOpen.set(false);
    this.getUsers(this.filterForm.get('email')?.value || '', this.currentPage, this.pageSize);
  }
  onPageChange(page: number) {
    this.getUsers(this.filterForm.get('email')?.value || '', page, this.pageSize);
  }
}
