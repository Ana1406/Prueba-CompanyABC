import { Component, OnInit } from '@angular/core';
import { SessionStorageItems } from '../../core/enums/session-storage.enum';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  userEmail: string = '';
  userRole: string = '';

  ngOnInit(): void {
    this.userEmail = sessionStorage.getItem(SessionStorageItems.USEREMAIL) || '';
    this.userRole = sessionStorage.getItem(SessionStorageItems.ROL) || '';
  }

}
