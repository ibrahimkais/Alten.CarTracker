import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Car } from '../models/Car';
import { ICarStatusChanged } from '../Interfaces/ICarStatusChanged';
import { CarStatus } from '../models/carStatus';
import { CarDisconnected } from '../models/carDisconnected';
import { SignalRService } from '../services/signal-rservice.service';
import { Statuses } from '../Interfaces/enums';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss']
})
export class VehicleComponent implements OnInit, ICarStatusChanged {
  isDisconnected = false;
  followCar = false;
  @Input() car: Car;
  @Output() zoomEvent = new EventEmitter();
  @Output() routesEvent = new EventEmitter();

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.subscribe(this);
    if (!this.car.carStatuses) { this.car.carStatuses = new Array<CarStatus>(); }
    this.isDisconnected = !this.car.lastStatusId || this.car.lastStatusId === Statuses.Disconnected;
  }

  public carStatusChanged(carStatus: CarStatus) {
    if (carStatus.vinCode === this.car.vin) {
      this.car.lastStatusId = carStatus.statusId;
      this.car.carStatuses.push(carStatus);
      this.isDisconnected = false;
      if (this.followCar) {
        this.zoom();
      }
    }
  }

  public carDisconnectedChanged(carDisconnected: CarDisconnected) {
    if (carDisconnected.vinCode === this.car.vin) {
      this.isDisconnected = true;
      this.car.lastStatusId = Statuses.Disconnected;
    }
  }

  zoom = (): void => this.zoomEvent.emit(null);

  follow = (): void => {
    this.followCar = !this.followCar;
    if (this.followCar) {
      this.zoom();
    }
  }

  showRoute = (): void => this.routesEvent.emit(null);
}
