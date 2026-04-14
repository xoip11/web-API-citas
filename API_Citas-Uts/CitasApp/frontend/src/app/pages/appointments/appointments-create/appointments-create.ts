import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { AppointmentService } from '../../../services/appointment.service';
import { Appointment } from '../../../models/appointment.model';

@Component({
  selector: 'app-appointments-create',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './appointments-create.html',
  styleUrls: ['./appointments-create.css']
})
export class AppointmentsCreateComponent {

  newAppointment: Appointment = {
    idCita: 0,
    idUsuarioEstudiante: 0,
    idEspecialista: 0,
    fecha: '',
    hora: '',
    descripcion: '',
    estado: ''
  };

  constructor(private appointmentService: AppointmentService) {}

  saveAppointment() {
    if (!this.newAppointment.descripcion || !this.newAppointment.fecha) {
    alert("Llena los campos obligatorios");
    return;
  }

  this.appointmentService.createAppointment(this.newAppointment)
    .subscribe(() => this.resetForm());
  }

  resetForm() {
    this.newAppointment = {
      idCita: 0,
      idUsuarioEstudiante: 0,
      idEspecialista: 0,
      fecha: '',
      hora: '',
      descripcion: '',
      estado: ''
    };
  }
  
}