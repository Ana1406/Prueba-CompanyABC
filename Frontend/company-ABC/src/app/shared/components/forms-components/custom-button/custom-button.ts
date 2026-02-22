import { Component, Input, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-custom-button',
  imports: [CommonModule],
  templateUrl: './custom-button.html',
  styleUrl: './custom-button.css',
})
export class CustomButton {
  @Input() set text(value: string) {
    this.textSignal.set(value);
  }
  @Input() color: 'primary' | 'ghost' | 'danger' | 'secondary' = 'primary';
  @Input() disabled: boolean = false;

  textSignal = signal('Enviar');

  buttonClass = computed(() => {
    const baseClass = 'custom-button';
    const colorClass = `custom-button--${this.color}`;
    return `${baseClass} ${colorClass}`;
  });
}
