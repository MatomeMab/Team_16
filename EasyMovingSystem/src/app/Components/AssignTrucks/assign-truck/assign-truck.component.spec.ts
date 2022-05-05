import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignTruckComponent } from './assign-truck.component';

describe('AssignTruckComponent', () => {
  let component: AssignTruckComponent;
  let fixture: ComponentFixture<AssignTruckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignTruckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignTruckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
