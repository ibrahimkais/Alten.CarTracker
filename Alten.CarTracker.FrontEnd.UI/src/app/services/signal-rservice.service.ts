import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { CarStatus } from '../models/carStatus';
import { CarDisconnected } from '../models/carDisconnected';
import Configuration from '../Data/configurations.json';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public carStatus: CarStatus;
  public carDisconnected: CarDisconnected;
  private statusHubConnection: signalR.HubConnection;
  private disconnectedHubConnection: signalR.HubConnection;

  constructor() {

  }
  public startConnection = () => {
    this.statusHubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(Configuration.serviceUrls.statusHubConnection)
                            .build();

    this.statusHubConnection
      .start()
      .then(() => console.log('Status Hub Connection started'))
      .catch(err => console.log('Error while starting Status Hub connection: ' + err));

    this.disconnectedHubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(Configuration.serviceUrls.disconnectedHubConnection)
                            .build();

    this.disconnectedHubConnection
    .start()
    .then(() => console.log('disconnected hub Connection started'))
    .catch(err => console.log('Error while starting disconnected hub  connection: ' + err));
  }

  public addSendStatusListener = () => {
    this.statusHubConnection.on('sendstatus', (data) => {
      this.carStatus = data;
      console.log(data);
    });
  }

  public addCarDisconnectedListener = () => {
    this.disconnectedHubConnection.on('vehicledissconnected', (data) => {
      this.carDisconnected = data;
      console.log(data);
    });
  }
}
