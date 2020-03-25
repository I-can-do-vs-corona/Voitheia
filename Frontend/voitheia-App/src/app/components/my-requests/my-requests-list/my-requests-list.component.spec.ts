import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRequestsListComponent } from './my-requests-list.component';

describe('MyRequestsListComponent', () => {
  let component: MyRequestsListComponent;
  let fixture: ComponentFixture<MyRequestsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyRequestsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyRequestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
