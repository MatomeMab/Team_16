import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateBackgroundStatusComponent } from './update-background-status.component';

describe('UpdateBackgroundStatusComponent', () => {
  let component: UpdateBackgroundStatusComponent;
  let fixture: ComponentFixture<UpdateBackgroundStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateBackgroundStatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateBackgroundStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
