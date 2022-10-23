import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateCocompanyComponent } from './update-cocompany.component';

describe('UpdateCocompanyComponent', () => {
  let component: UpdateCocompanyComponent;
  let fixture: ComponentFixture<UpdateCocompanyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateCocompanyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateCocompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
