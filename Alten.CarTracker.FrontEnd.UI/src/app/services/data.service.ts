import { Injectable } from '@angular/core';
import VLUR4X20009048044 from '../Data/VLUR4X20009048044_long.json';
import VLUR4X20009048066 from '../Data/VLUR4X20009048066_long.json';
import VLUR4X20009093588 from '../Data/VLUR4X20009093588_long.json';
import YS2R4X20005387055 from '../Data/YS2R4X20005387055_long.json';
import YS2R4X20005387949 from '../Data/YS2R4X20005387949_long.json';
import YS2R4X20005388011 from '../Data/YS2R4X20005388011_long.json';
import YS2R4X20005399401 from '../Data/YS2R4X20005399401_long.json';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  carsData: any[] = [];

  constructor() {
    this.carsData.push({ carId: 'VLUR4X20009048044', data: VLUR4X20009048044 });
    this.carsData.push({ carId: 'VLUR4X20009048066', data: VLUR4X20009048066 });
    this.carsData.push({ carId: 'VLUR4X20009093588', data: VLUR4X20009093588 });
    this.carsData.push({ carId: 'YS2R4X20005387055', data: YS2R4X20005387055 });
    this.carsData.push({ carId: 'YS2R4X20005387949', data: YS2R4X20005387949 });
    this.carsData.push({ carId: 'YS2R4X20005388011', data: YS2R4X20005388011 });
    this.carsData.push({ carId: 'YS2R4X20005399401', data: YS2R4X20005399401 });
  }

  getData(carId: string) {
    return this.carsData.find(car => car.carId === carId);
  }
}
