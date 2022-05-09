import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeTypelistComponent } from './employee-typelist.component';

describe('EmployeeTypelistComponent', () => {
  let component: EmployeeTypelistComponent;
  let fixture: ComponentFixture<EmployeeTypelistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeTypelistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeTypelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
