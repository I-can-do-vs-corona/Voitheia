import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRequestsViewComponent } from './my-requests-view.component';

describe('MyRequestsViewComponent', () => {
  let component: MyRequestsViewComponent;
  let fixture: ComponentFixture<MyRequestsViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyRequestsViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyRequestsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
