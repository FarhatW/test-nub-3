import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersChildViewComponent } from './users-child-view.component';

describe('UsersChildViewComponent', () => {
  let component: UsersChildViewComponent;
  let fixture: ComponentFixture<UsersChildViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersChildViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersChildViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
