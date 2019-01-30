import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { SignalRService } from './services/signal-rservice.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Alten Car Tracker';

  public constructor(private titleService: Title,
    private signalRService: SignalRService
  ) { }

  ngOnInit() {
    this.titleService.setTitle(this.title);
    this.signalRService.startConnection();
    this.signalRService.addSendStatusListener();
    this.signalRService.addCarDisconnectedListener();
  }
}
