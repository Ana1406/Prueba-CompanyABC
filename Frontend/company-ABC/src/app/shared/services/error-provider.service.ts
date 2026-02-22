import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ValidationErrorMessages } from '../../core/enums/validation-error-messages.enum';


@Injectable({
  providedIn: 'root',
})
export class ErrorProviderService {
  constructor() { }

  isInvalidField(form: FormGroup, field: string): boolean | undefined {
    return (form.get(field)?.touched || form.get(field)?.dirty) && !form.get(field)?.valid;
  }

  getErrorMessage(form: FormGroup, field: string, extra: string = ''): Observable<string> {
    let message: Observable<string> = new Observable(subcription => subcription.next(''));
    let control = form.get(field);

    if (control) {
      if (control.hasError('required')) message = of(ValidationErrorMessages.REQUIRED);
      else if (control.hasError('email')) message = of(ValidationErrorMessages.INVALID_EMAIL);
      else if (control.hasError('password-weakness'))
        message = of(ValidationErrorMessages.WEAK_PASSWORD);
    }

    return message;
  }
}
