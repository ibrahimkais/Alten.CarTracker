import { TestBed } from '@angular/core/testing';

import { SignalRService } from './signal-rservice.service';

describe('SignalRServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SignalRService = TestBed.get(SignalRService);
    expect(service).toBeTruthy();
  });
});
