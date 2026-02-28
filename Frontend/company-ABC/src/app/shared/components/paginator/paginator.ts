import { CommonModule } from '@angular/common';
import { Component, computed, input, output, signal } from '@angular/core';

@Component({
  selector: 'app-paginator',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './paginator.html',
  styleUrls: ['./paginator.css'],
})
export class Paginator {
  totalRecords = input.required<number>();
  pageSize = input.required<number>();

  // ✅ Output moderno
  pageChange = output<number>();

  // Estado interno
  currentPage = signal(1);

  // Computeds reactivos automáticos
  totalPages = computed(() =>
    Math.ceil(this.totalRecords() / this.pageSize())
  );

  pages = computed(() =>
    Array.from({ length: this.totalPages() }, (_, i) => i + 1)
  );

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages()) return;

    this.currentPage.set(page);
    this.pageChange.emit(page);
  }

  next() {
    this.goToPage(this.currentPage() + 1);
  }

  previous() {
    this.goToPage(this.currentPage() - 1);
  }
}
