import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersJceViewComponent } from './users-jce-view.component';

describe('UsersJceViewComponent', () => {
  let component: UsersJceViewComponent;
  let fixture: ComponentFixture<UsersJceViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersJceViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersJceViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
