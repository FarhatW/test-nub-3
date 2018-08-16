import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CesListPerUserComponent } from './ces-list-per-user.component';

describe('CesListPerUserComponent', () => {
  let component: CesListPerUserComponent;
  let fixture: ComponentFixture<CesListPerUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CesListPerUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CesListPerUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
