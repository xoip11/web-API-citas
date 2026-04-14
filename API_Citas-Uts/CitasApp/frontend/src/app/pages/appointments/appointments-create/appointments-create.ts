import { Component } from '@angular/core';
import { AppointmentService } from '../../../services/appointment.service';
import { Appointment } from '../../../models/appointment.model';

@Component({
  selector: 'app-appointments-create',
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

}