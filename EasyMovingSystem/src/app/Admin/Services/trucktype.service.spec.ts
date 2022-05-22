import { TestBed } from '@angular/core/testing';

import { TrucktypeService } from './trucktype.service';

describe('TrucktypeService', () => {
  let service: TrucktypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrucktypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
