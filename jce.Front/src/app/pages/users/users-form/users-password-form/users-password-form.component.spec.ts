import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersPasswordFormComponent } from './users-password-form.component';

describe('UsersPasswordFormComponent', () => {
  let component: UsersPasswordFormComponent;
  let fixture: ComponentFixture<UsersPasswordFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersPasswordFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersPasswordFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
