import { Component, OnInit, ViewChild } from '@angular/core';
import { SidenavService } from '../services/SideNavService';
import { MatSidenav } from '@angular/material';

@Component({
  selector: 'app-data-filter',
  templateUrl: './data-filter.component.html',
  styleUrls: ['./data-filter.component.scss']
})
export class DataFilterComponent implements OnInit {

  @ViewChild('sidenav') public sidenav: MatSidenav;

  astronaut = 'confirmed';
  events: string[] = [];
  opened = false;

  statuses: any[] = [];
  customers: any[] = [];
  customer: any;
  status: any;

  constructor(private sidenavService: SidenavService) {

  }

  ngOnInit() {
    this.sidenavService.setSidenav(this.sidenav);
  }
}
