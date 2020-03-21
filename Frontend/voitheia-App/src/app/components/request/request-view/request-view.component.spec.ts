import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestViewComponent } from './request-view.component';

describe('RequestViewComponent', () => {
  let component: RequestViewComponent;
  let fixture: ComponentFixture<RequestViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
