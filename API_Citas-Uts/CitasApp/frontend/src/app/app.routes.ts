import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { AppointmentsListComponent } from './pages/appointments/appointments-list/appointments-list';
import { AppointmentsCreateComponent } from './pages/appointments/appointments-create/appointments-create';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'citas', component: AppointmentsListComponent },
  { path: 'crear-cita', component: AppointmentsCreateComponent }
];