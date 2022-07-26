import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReqQuoteComponent } from './req-quote.component';

describe('ReqQuoteComponent', () => {
  let component: ReqQuoteComponent;
  let fixture: ComponentFixture<ReqQuoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReqQuoteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReqQuoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
