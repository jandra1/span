import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateCobjectComponent } from './update-cobject.component';

describe('UpdateCobjectComponent', () => {
  let component: UpdateCobjectComponent;
  let fixture: ComponentFixture<UpdateCobjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateCobjectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateCobjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
