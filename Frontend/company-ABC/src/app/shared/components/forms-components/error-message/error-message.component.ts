import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';
import { Component, inject, Input, OnInit } from '@angular/core';
import { ErrorProviderService } from '../../../services/error-provider.service';


@Component({
  imports: [CommonModule],
  providers: [ErrorProviderService],
  selector: 'app-err-msg',
  templateUrl: './error-message.component.html',
  styleUrls: ['./error-message.component.css'],
})
export class ErrorMessageComponent {
  @Input() formGroup: FormGroup = new FormGroup({});
  @Input() field: string = '';
  @Input() extra: string = '';

  private errorProviderService: ErrorProviderService = inject(ErrorProviderService);

  constructor() { }

  get isInvalidField() {
    return this.errorProviderService.isInvalidField(this.formGroup, this.field);
  }

  get errorMessage() {
    return this.errorProviderService.getErrorMessage(this.formGroup, this.field, this.extra);
  }
}
