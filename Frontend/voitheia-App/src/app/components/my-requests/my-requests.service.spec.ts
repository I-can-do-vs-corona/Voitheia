import { TestBed } from '@angular/core/testing';

import { MyRequestsService } from './my-requests.service';

describe('MyRequestsService', () => {
  let service: MyRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
