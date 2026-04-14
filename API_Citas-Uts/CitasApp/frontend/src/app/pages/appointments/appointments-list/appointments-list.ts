import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

import { AppointmentService } from '../../../services/appointment.service';
import { Appointment } from '../../../models/appointment.model';

@Component({
  selector: 'app-appointments-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule
  ],
  templateUrl: './appointments-list.html',
  styleUrls: ['./appointments-list.css']
})
export class AppointmentsListComponent implements OnInit {

  appointments: Appointment[] = [];

  displayedColumns: string[] = [
    'idCita', 'idUsuarioEstudiante', 'idEspecialista',
    'fecha', 'hora', 'descripcion', 'estado', 'actions'
  ];

  constructor(private service: AppointmentService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAppointments()
      .subscribe(data => {
        console.log(data);  
        this.appointments = data});
  }

  deleteAppointment(id: number) {
    this.service.deleteAppointment(id)
      .subscribe(() => this.load());
  }

  confirmAppointment(id: number) {
    this.service.confirmAppointment(id)
      .subscribe(() => this.load());
  }
}