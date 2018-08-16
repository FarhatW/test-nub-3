import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersListSearchComponent } from './users-list-search.component';

describe('UsersListSearchComponent', () => {
  let component: UsersListSearchComponent;
  let fixture: ComponentFixture<UsersListSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersListSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersListSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
