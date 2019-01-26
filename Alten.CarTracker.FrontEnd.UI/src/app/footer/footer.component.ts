import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  footerValue: string;

  constructor() { }

  ngOnInit() {
    this.footerValue = `Copy Right © ${new Date().getFullYear()}`;
  }

}
