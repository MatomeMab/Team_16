import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestQouteListComponent } from './request-qoute-list.component';

describe('RequestQouteListComponent', () => {
  let component: RequestQouteListComponent;
  let fixture: ComponentFixture<RequestQouteListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestQouteListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestQouteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
