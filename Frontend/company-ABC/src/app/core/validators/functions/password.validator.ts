import { AbstractControl } from '@angular/forms';

const passwordRegEx: RegExp = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).*$/;

export function passwordValidator(control: AbstractControl): { [key: string]: boolean } | null {
  let value: string = control.value;
  if (!!value && !passwordRegEx.test(value)) return { 'password-weakness': true };

  return null;
}
