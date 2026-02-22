import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phone',
})
export class PhonePipe implements PipeTransform {

  transform(value: string): string {
    if (!value) return '';

    const parts = value.split(/x/i);
    const mainNumber = parts[0];
    const extension = parts[1];

    const digits = mainNumber.replace(/\D/g, '');

    let formatted = digits;

    if (digits.length === 10) {
      formatted = `(${digits.slice(0, 3)}) ${digits.slice(3, 6)}-${digits.slice(6)}`;
    }

    if (extension) {
      formatted += ` ext. ${extension.replace(/\D/g, '')}`;
    }

    return formatted;
  }

}
