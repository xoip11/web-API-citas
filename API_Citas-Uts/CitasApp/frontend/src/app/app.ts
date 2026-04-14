import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";

import { AppointmentsListComponent } from "./pages/appointments/appointments-list/appointments-list";
import { AppointmentsCreateComponent } from "./pages/appointments/appointments-create/appointments-create";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
    AppointmentsListComponent,
    AppointmentsCreateComponent
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent {}