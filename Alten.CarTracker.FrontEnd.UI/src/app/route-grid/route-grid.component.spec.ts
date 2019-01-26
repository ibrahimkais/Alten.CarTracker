import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RouteGridComponent } from './route-grid.component';

describe('RouteGridComponent', () => {
  let component: RouteGridComponent;
  let fixture: ComponentFixture<RouteGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RouteGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RouteGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
