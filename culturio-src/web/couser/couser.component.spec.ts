import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CouserComponent } from './couser.component';

describe('CouserComponent', () => {
  let component: CouserComponent;
  let fixture: ComponentFixture<CouserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CouserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CouserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
