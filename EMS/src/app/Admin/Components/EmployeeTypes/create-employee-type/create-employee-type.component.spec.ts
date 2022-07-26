import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeTypeComponent } from './create-employee-type.component';

describe('CreateEmployeeTypeComponent', () => {
  let component: CreateEmployeeTypeComponent;
  let fixture: ComponentFixture<CreateEmployeeTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEmployeeTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEmployeeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
