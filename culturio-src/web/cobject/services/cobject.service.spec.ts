import { TestBed } from '@angular/core/testing';

import { CobjectService } from './Cobject.service';

describe('CobjectService', () => {
  let service: CobjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CobjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});