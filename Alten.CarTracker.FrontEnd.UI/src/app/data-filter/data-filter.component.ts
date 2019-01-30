import { Component, OnInit, ViewChild } from '@angular/core';
import { SidenavService } from '../services/SideNavService';
import { MatSidenav } from '@angular/material';
import { BackEndService } from '../services/back-end-service.service';
import { Customer } from '../models/Customer';
import { StatusLookup } from '../models/StatusLookup';
import { Car } from '../models/Car';

@Component({
  selector: 'app-data-filter',
  templateUrl: './data-filter.component.html',
  styleUrls: ['./data-filter.component.scss']
})
export class DataFilterComponent implements OnInit {

  @ViewChild('sidenav') public sidenav: MatSidenav;
  opened = true;

  statuses: StatusLookup[] = [];
  customers: Customer[] = [];
  cars: Car[] = [];
  customer: Customer;
  status: StatusLookup;
  selectedCar: Car;

  constructor(private sidenavService: SidenavService, private backEndService: BackEndService) {

  }

  ngOnInit() {

    this.sidenavService.setSidenav(this.sidenav);

    this.backEndService.getCustomers().subscribe(data => {
      this.customers = data;
    });

    this.backEndService.getCars().subscribe(data => {
      this.cars = data;
    });

    this.backEndService.getCarsStatsesLookup().subscribe(data => {
      this.statuses = data;
    });
  }
}
