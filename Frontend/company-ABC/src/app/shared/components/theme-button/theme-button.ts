import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faMoon, faSun } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-theme-button',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './theme-button.html',
  styleUrl: './theme-button.css',
})
export class ThemeButton {
  faSun = faSun;
  faMoon = faMoon;
  isDarkMode: boolean = false;

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    const theme = this.isDarkMode ? 'dark' : 'light';

    document.documentElement.setAttribute('data-theme', theme);

    localStorage.setItem('theme', theme);
  }
}
