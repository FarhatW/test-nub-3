import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersIdentityViewComponent } from './users-identity-view.component';

describe('UsersIdentityViewComponent', () => {
  let component: UsersIdentityViewComponent;
  let fixture: ComponentFixture<UsersIdentityViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersIdentityViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersIdentityViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
