import { Component, EventEmitter, forwardRef, input, Input, Output, signal } from '@angular/core';
import { ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ErrorMessageComponent } from '../error-message/error-message.component';
import { Items } from '../../../../core/models/catalog.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-custom-select',
  imports: [ErrorMessageComponent, CommonModule],
  templateUrl: './custom-select.html',
  styleUrl: './custom-select.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomSelect),
      multi: true,
    },
  ],
})
export class CustomSelect implements ControlValueAccessor {
  @Input() set label(value: string) { this.labelSignal.set(value); }
  @Input() id: string = '';
  @Input() placeholder: string = 'Seleccione...';
  options = input.required<Items[]>();

  // Para ErrorMessageComponent
  formControlName = input.required<string>();
  form = input.required<FormGroup>();

  labelSignal = signal('');

  value: string | null = null;
  disabled = false;

  private onChange: (value: any) => void = () => { };
  private onTouched: () => void = () => { };

  writeValue(obj: any): void {
    this.value = obj ?? null;
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onSelectChange(event: any): void {
    this.value = event.target.value;
    this.onChange(this.value);
    this.onTouched();
  }
}
