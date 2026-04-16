import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { AuthService } from '../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './login.html'
})
export class LoginComponent {

  data = {
    email: '',
    password: ''
  };

  constructor(private auth: AuthService) {}

  login() {
    this.auth.login(this.data).subscribe({
      next: (res) => {
        console.log('LOGIN OK', res);
        localStorage.setItem('token', res.token); // opcional
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}