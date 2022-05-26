import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeTypeUpdateComponent } from './employee-type-update.component';

describe('EmployeeTypeUpdateComponent', () => {
  let component: EmployeeTypeUpdateComponent;
  let fixture: ComponentFixture<EmployeeTypeUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeTypeUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeTypeUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
