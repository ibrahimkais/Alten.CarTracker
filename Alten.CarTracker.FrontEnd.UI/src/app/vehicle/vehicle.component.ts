import { Component, OnInit, Input } from '@angular/core';
import { Car } from '../models/Car';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss']
})
export class VehicleComponent implements OnInit {

  @Input() car: Car;
  constructor() { }

  ngOnInit() {
  }

}
