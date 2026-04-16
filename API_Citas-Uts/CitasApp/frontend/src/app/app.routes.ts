import { Routes } from '@angular/router';

import { Login } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { AppointmentsListComponent } from './pages/appointments/appointments-list/appointments-list';
import { AppointmentsCreateComponent } from './pages/appointments/appointments-create/appointments-create';

import { CreateSpecialistComponent } from './pages/specialists/create-specialist.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: Login },
  { path: 'register', component: RegisterComponent },

  { path: 'citas', component: AppointmentsListComponent },
  { path: 'crear-cita', component: AppointmentsCreateComponent },

  { path: 'create-specialist', component: CreateSpecialistComponent }
];