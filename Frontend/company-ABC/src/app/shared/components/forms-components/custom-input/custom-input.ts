import { Component, input, Input, signal, forwardRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ErrorMessageComponent } from '../error-message/error-message.component';

@Component({
  selector: 'app-custom-input',
  imports: [CommonModule, ReactiveFormsModule, ErrorMessageComponent],
  templateUrl: './custom-input.html',
  styleUrl: './custom-input.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomInput),
      multi: true
    }
  ]
})
export class CustomInput implements ControlValueAccessor {
  @Input() set label(value: string) {
    this.labelSignal.set(value);
  }
  @Input() set type(value: string) {
    this.typeSignal.set(value);
  }
  @Input() placeholder: string = '';
  @Input() id: string = '';

  labelSignal = signal('');
  typeSignal = signal('text');
  formControlName = input.required<string>();
  form = input.required<FormGroup>();

  private onChange: (value: any) => void = () => { };
  private onTouched: () => void = () => { };
  inputValue: string = '';

  writeValue(obj: any): void {
    this.inputValue = obj || '';
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
  }

  onInputChange(event: any): void {
    this.inputValue = event.target.value;
    this.onChange(this.inputValue);
  }

  onInputBlur(): void {
    this.onTouched();
  }
}
