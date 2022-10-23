import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CocompanyComponent } from './cocompany.component';

describe('CocompanyComponent', () => {
  let component: CocompanyComponent;
  let fixture: ComponentFixture<CocompanyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CocompanyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CocompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
