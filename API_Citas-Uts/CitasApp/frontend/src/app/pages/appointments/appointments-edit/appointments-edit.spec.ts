import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentsEdit } from './appointments-edit';

describe('AppointmentsEdit', () => {
  let component: AppointmentsEdit;
  let fixture: ComponentFixture<AppointmentsEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppointmentsEdit],
    }).compileComponents();

    fixture = TestBed.createComponent(AppointmentsEdit);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
