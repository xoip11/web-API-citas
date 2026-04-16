import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { AuthService } from '../../services/auth.service';

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
export class Login {

  data = {
    email: '',
    password: ''
  };

  constructor(private authService: AuthService) {}

  login() {
   this.authService.login(this.data).subscribe({
  next: (res) => {
    console.log(res);
    localStorage.setItem('token', res.token);
  },
  error: (err) => {
    console.error(err);
  }
});
  }
  
}