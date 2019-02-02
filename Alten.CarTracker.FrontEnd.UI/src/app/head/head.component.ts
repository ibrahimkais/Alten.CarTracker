import { Component, OnInit } from '@angular/core';
import { SidenavService } from '../services/SideNavService';
import { SignalRService } from '../services/signal-rservice.service';
import { ICarStatusChanged } from '../Interfaces/ICarStatusChanged';
import { CarStatus } from '../models/carStatus';
import { CarDisconnected } from '../models/carDisconnected';

@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.scss']
})
export class HeadComponent implements OnInit, ICarStatusChanged {
  appHeader: string;
  creator: string;
  lastUpdate: Date;

  constructor(private sidenav: SidenavService, private signalRService: SignalRService) {

  }

  public carStatusChanged = (carStatus: CarStatus) => this.lastUpdate = new Date();

  public carDisconnectedChanged = (carDisconnected: CarDisconnected) => this.lastUpdate = new Date();

  toggleRightSidenav() {
    this.sidenav.toggle();
 }

  ngOnInit() {
    this.signalRService.subscribe(this);
    this.appHeader = 'Alten Car Tracker';
    this.creator = 'by Ibrahim Kais';
  }
}
