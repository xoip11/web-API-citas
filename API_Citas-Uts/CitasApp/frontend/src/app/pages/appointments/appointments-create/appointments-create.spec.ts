import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentsCreateComponent } from './appointments-create';

describe('AppointmentsCreate', () => {
  let component: AppointmentsCreateComponent;
  let fixture: ComponentFixture<AppointmentsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppointmentsCreateComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AppointmentsCreateComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
