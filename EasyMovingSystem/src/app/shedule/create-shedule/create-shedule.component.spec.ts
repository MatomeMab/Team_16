import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSheduleComponent } from './create-shedule.component';

describe('CreateSheduleComponent', () => {
  let component: CreateSheduleComponent;
  let fixture: ComponentFixture<CreateSheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateSheduleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
