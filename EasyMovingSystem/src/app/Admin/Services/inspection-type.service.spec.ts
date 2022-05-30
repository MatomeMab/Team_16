import { TestBed } from '@angular/core/testing';

import { InspectionTypeService } from './inspection-type.service';

describe('InspectionTypeService', () => {
  let service: InspectionTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InspectionTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
