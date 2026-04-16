import { TestBed } from '@angular/core/testing';
import { Component } from '@angular/core';
import { AppointmentService } from '../app/services/appointment.service';
import { Appointment } from '../../src/app/models/appointment.model';
import { AppComponent } from './app';

@Component({
  selector: 'app-appointments-create',
  templateUrl: '../app/pages/appointments/appointments-create/appointments-create.html',
  styleUrls: ['../app/pages/appointments/appointments-create/appointments-create.css']
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

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', async () => {
    const fixture = TestBed.createComponent(AppComponent);
    await fixture.whenStable();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hello, frontend');
  });
});
