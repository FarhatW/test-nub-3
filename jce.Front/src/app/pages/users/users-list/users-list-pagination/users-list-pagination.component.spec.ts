import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersListPaginationComponent } from './users-list-pagination.component';

describe('UsersListPaginationComponent', () => {
  let component: UsersListPaginationComponent;
  let fixture: ComponentFixture<UsersListPaginationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersListPaginationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersListPaginationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
