import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Appointment } from "../models/appointment.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AppointmentService {

  private apiUrl = 'https://localhost:7140/api/Appointment';

  constructor(private http: HttpClient) {}

  getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/Listar`);
  }

  createAppointment(cita: Appointment) {
    return this.http.post(`${this.apiUrl}/Crear`, cita);
  }

  updateAppointment(cita: Appointment) {
    return this.http.put(`${this.apiUrl}/Actualizar`, cita);
  }

  deleteAppointment(id: number) {
    return this.http.delete(`${this.apiUrl}/Borrar/${id}`);
  }

  confirmAppointment(id: number) {
    return this.http.put(`${this.apiUrl}/confirmar/${id}`, {});
  }
}