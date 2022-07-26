import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewReqQuoteComponent } from './view-req-quote.component';

describe('ViewReqQuoteComponent', () => {
  let component: ViewReqQuoteComponent;
  let fixture: ComponentFixture<ViewReqQuoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewReqQuoteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewReqQuoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
