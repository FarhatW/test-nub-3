import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedChildrenListComponent } from './shared-children-list.component';

describe('SharedChildrenListComponent', () => {
  let component: SharedChildrenListComponent;
  let fixture: ComponentFixture<SharedChildrenListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SharedChildrenListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedChildrenListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
