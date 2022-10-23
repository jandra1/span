import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PAdminUsersComponent } from './p-admin-users.component';

describe('PAdminUsersComponent', () => {
  let component: PAdminUsersComponent;
  let fixture: ComponentFixture<PAdminUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PAdminUsersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PAdminUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
