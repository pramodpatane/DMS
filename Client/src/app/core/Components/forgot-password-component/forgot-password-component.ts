import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-forgot-password-component',
  imports: [FormsModule, CommonModule,],
  templateUrl: './forgot-password-component.html',
})
export class ForgotPasswordComponent implements OnInit {

  useremail: string = '';

  constructor() { }

  ngOnInit(): void {
  }

  sendResetLink() {
    // Logic to send reset link to the provided email address
    console.log(`Sending reset link to ${this.useremail}`);
  }

}
