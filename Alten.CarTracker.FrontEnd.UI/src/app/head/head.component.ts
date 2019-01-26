import { Component, OnInit } from '@angular/core';
import { SidenavService } from '../services/SideNavService';

@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.scss']
})
export class HeadComponent implements OnInit {

  appHeader: string;
  creator: string;

  constructor(private sidenav: SidenavService) {

  }

  toggleRightSidenav() {
    this.sidenav.toggle();
 }

  ngOnInit() {
    this.appHeader = 'Alten Car Tracker';
    this.creator = 'by Ibrahim Kais';
  }
}
