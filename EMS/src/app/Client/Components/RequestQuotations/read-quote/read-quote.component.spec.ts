import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadQuoteComponent } from './read-quote.component';

describe('ReadQuoteComponent', () => {
  let component: ReadQuoteComponent;
  let fixture: ComponentFixture<ReadQuoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReadQuoteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadQuoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
