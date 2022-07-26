import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestedQuoteListComponent } from './requested-quote-list.component';

describe('RequestedQuoteListComponent', () => {
  let component: RequestedQuoteListComponent;
  let fixture: ComponentFixture<RequestedQuoteListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestedQuoteListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestedQuoteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
