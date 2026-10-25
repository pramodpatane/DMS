import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-purchase',
  imports: [CommonModule, FormsModule],
  templateUrl: './purchase.html',
  styleUrl: './purchase.css',
})
export class Purchase {
  defaultView: boolean = true;

  toggleView() {
    this.defaultView = !this.defaultView;
  }
}
