import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MapControlService {

  public drawRoute: any;
  public locate: any;
  public clear: any;

  constructor() { }

  setDrawRoute = (drawFunc: any): void => this.drawRoute = drawFunc;

  setLocatePoint = (locateFunc: any): void => this.locate = locateFunc;

  setClear = (clearFunc: any): void => this.clear = clearFunc;
}
