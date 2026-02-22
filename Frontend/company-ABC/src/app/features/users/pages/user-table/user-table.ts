import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user-service';
import { PhonePipe } from '../../../../core/pipes/phone-pipe';

@Component({
  selector: 'app-user-table',
  imports: [CommonModule, PhonePipe],
  templateUrl: './user-table.html',
  styleUrl: './user-table.css',
})
export class UserTable implements OnInit {
  userList: User[] = [];
  private userService: UserService = inject(UserService);
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);
  ngOnInit(): void {
    this.getUsers();

  }
  getUsers() {
    this.userService.getUsers().subscribe({
      next: (response: User[]) => {
        this.userList = response;
        this.cdr.detectChanges();
        console.log('Users fetched successfully:', this.userList);
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      }
    });
  }
}
