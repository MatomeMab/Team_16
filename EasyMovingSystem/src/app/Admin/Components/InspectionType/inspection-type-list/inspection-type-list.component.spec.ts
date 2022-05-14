import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionTypeListComponent } from './inspection-type-list.component';

describe('InspectionTypeListComponent', () => {
  let component: InspectionTypeListComponent;
  let fixture: ComponentFixture<InspectionTypeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InspectionTypeListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
