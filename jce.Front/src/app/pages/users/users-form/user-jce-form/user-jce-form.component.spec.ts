import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserJceFormComponent } from './user-jce-form.component';

describe('UserJceFormComponent', () => {
  let component: UserJceFormComponent;
  let fixture: ComponentFixture<UserJceFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserJceFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserJceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
