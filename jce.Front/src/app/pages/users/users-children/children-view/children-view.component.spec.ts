import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildrenViewComponent } from './children-view.component';

describe('ChildrenViewComponent', () => {
  let component: ChildrenViewComponent;
  let fixture: ComponentFixture<ChildrenViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChildrenViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChildrenViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
