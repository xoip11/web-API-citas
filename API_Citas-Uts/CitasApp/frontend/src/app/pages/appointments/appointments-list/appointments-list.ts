import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Appointment } from '../../../models/appointment.model';
import { AppointmentService } from '../../../services/appointment.service';

@Component({
  selector: 'app-appointments-list',
  templateUrl: '../appointments-list/appointments-list.html',
  styleUrls: ['../appointments-list/appointments-list.css']
})
export class AppointmentsListComponent implements OnInit {

  appointments: Appointment[] = [];

  displayedColumns: string[] = [
    'idCita', 'idUsuarioEstudiante', 'idEspecialista',
    'fecha', 'hora', 'descripcion', 'estado', 'actions'
  ];

  constructor(
    private appointmentService: AppointmentService,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.appointmentService.getAppointments()
      .subscribe(data => {
        this.appointments = data;
        this.cd.detectChanges();
      });
  }

  deleteAppointment(id: number) {
    this.appointmentService.deleteAppointment(id)
      .subscribe(() => this.loadAppointments());
  }

  confirmAppointment(id: number) {
    this.appointmentService.confirmAppointment(id)
      .subscribe(() => this.loadAppointments());
  }

  editAppointment(cita: Appointment) {
    // Aquí puedes navegar a appointments-edit pasando el id
    // Ejemplo: this.router.navigate(['/appointments/edit', cita.idCita]);
  }
}