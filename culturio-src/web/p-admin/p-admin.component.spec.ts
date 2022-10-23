import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PAdminComponent } from './p-admin.component';

describe('PAdminComponent', () => {
  let component: PAdminComponent;
  let fixture: ComponentFixture<PAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PAdminComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
