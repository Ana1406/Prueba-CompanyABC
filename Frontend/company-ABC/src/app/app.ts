import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ThemeButton } from './shared/components/theme-button/theme-button';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ThemeButton],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('company-ABC');
}
