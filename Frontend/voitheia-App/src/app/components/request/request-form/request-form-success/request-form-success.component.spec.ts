import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestFormSuccessComponent } from './request-form-success.component';

describe('RequestFormSuccessComponent', () => {
  let component: RequestFormSuccessComponent;
  let fixture: ComponentFixture<RequestFormSuccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestFormSuccessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestFormSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
