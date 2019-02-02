import { TestBed } from '@angular/core/testing';

import { MapControlService } from './map-control.service';

describe('MapControlService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MapControlService = TestBed.get(MapControlService);
    expect(service).toBeTruthy();
  });
});
