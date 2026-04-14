import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Appointment } from "./models/appointment.model";
import { AppointmentService } from "./services/appointment.service";
import { ChangeDetectorRef } from "@angular/core";
//---------------------------------------------------------------------//
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { MatInputModule } from "@angular/material/input";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";

@Component({
  selector: 'app-appointments-list',
  templateUrl: '../app/pages/appointments/appointments-list/appointments-list.html',
  styleUrls: ['../app/pages/appointments/appointments-list/appointments-list.css']
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
    
  }
}
@Component({
    selector: 'app-root',
    imports: [
        CommonModule,
        FormsModule,
        MatTableModule,
        MatButtonModule,
        MatInputModule,
        MatCardModule,
        MatFormFieldModule
    ],
    templateUrl: './app.html',
    styleUrl: './app.css'
})
export class app implements OnInit{
    Appointment: Appointment[] = [];

     newAppointment: Appointment = {
    idCita: 0,
    idUsuarioEstudiante: 0,
    idEspecialista: 0,
    fecha: '',
    hora: '',
    descripcion: '',
    estado: ''
  };
  constructor(private appointmentService: AppointmentService) {
    
  }

  saveAppointment() {
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

