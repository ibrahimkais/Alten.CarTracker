import { Component, OnInit, Input } from '@angular/core';
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
  @Input() car: Car;

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
    }
  }

  public carDisconnectedChanged(carDisconnected: CarDisconnected) {
    if (carDisconnected.vinCode === this.car.vin) {
      this.isDisconnected = true;
      this.car.lastStatusId = Statuses.Disconnected;
    }
  }

  zoom = () => {

  }

  follow = () => {

  }

  showRoute = () => {

  }
}
