import { TestBed } from '@angular/core/testing';
import { CocompanyService } from './cocompany.service';



describe('CocompanyService', () => {
  let service: CocompanyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CocompanyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
