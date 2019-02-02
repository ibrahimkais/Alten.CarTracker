import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Car } from '../models/Car';
import { CarStatus } from '../models/carStatus';
import { SignalRService } from '../services/signal-rservice.service';
import { ICarStatusChanged } from '../Interfaces/ICarStatusChanged';
import { CarDisconnected } from '../models/carDisconnected';
import { MapControlService } from '../services/map-control.service';

@Component({
  selector: 'app-route-grid',
  templateUrl: './route-grid.component.html',
  styleUrls: ['./route-grid.component.scss']
})
export class RouteGridComponent implements OnInit, ICarStatusChanged {
  car: Car = new Car();
  isHidden = true;
  dataSource: Array<CarStatus> = [];
  displayedColumns: string[] = ['actions', 'receivedDate', 'x', 'y'];

  constructor(private signalRService: SignalRService, private mapControlService: MapControlService) { }

  ngOnInit() { this.signalRService.subscribe(this); }

  show = (car: Car) => {
    this.car = car;
    this.dataSource = [...this.car.carStatuses];
    this.isHidden = false;
  }

  hide = () => {
    this.isHidden = true;
    this.car = new Car();
    this.mapControlService.clear();
  }

  draw = (): void => this.mapControlService.drawRoute(this.car);

  locate = (status: CarStatus): void => this.mapControlService.locate(status);

  public carStatusChanged(carStatus: CarStatus) {
    if (carStatus.vinCode === this.car.vin) {
      setTimeout(() => {
        this.dataSource = [...this.car.carStatuses];
      }, 100);
    }
  }

  public carDisconnectedChanged(carDisconnected: CarDisconnected) { }
}
