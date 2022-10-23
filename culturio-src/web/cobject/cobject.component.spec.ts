import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CobjectComponent } from './cobject.component';

describe('CobjectComponent', () => {
  let component: CobjectComponent;
  let fixture: ComponentFixture<CobjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CobjectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CobjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
