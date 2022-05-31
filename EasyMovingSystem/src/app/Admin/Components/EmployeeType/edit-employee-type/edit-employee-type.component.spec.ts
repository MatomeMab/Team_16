import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmployeeTypeComponent } from './edit-employee-type.component';

describe('EditEmployeeTypeComponent', () => {
  let component: EditEmployeeTypeComponent;
  let fixture: ComponentFixture<EditEmployeeTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditEmployeeTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEmployeeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
