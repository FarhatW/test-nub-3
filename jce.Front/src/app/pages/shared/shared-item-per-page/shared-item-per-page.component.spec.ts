import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedItemPerPageComponent } from './shared-item-per-page.component';

describe('SharedItemPerPageComponent', () => {
  let component: SharedItemPerPageComponent;
  let fixture: ComponentFixture<SharedItemPerPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SharedItemPerPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedItemPerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
