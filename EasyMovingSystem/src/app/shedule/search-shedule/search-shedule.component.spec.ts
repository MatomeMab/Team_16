import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchSheduleComponent } from './search-shedule.component';

describe('SearchSheduleComponent', () => {
  let component: SearchSheduleComponent;
  let fixture: ComponentFixture<SearchSheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchSheduleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchSheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
