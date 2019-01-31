import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { CarStatus } from '../models/carStatus';
import { CarDisconnected } from '../models/carDisconnected';
import Configuration from '../Data/configurations.json';
import { ICarStatusChanged } from '../Interfaces/ICarStatusChanged';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private statusHubConnection: signalR.HubConnection;
  private disconnectedHubConnection: signalR.HubConnection;

  private subscribers: Array<ICarStatusChanged>;

  constructor() {
    this.subscribers = new Array<ICarStatusChanged>();
  }

  public subscribe = (subscriber: ICarStatusChanged) => {
    this.subscribers.push(subscriber);
  }

  public startConnection = () => {
    this.statusHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(Configuration.serviceUrls.statusHubConnection)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.statusHubConnection
      .start()
      .then(() => console.log('Status Hub Connection started'))
      .catch(err => console.log('Error while starting Status Hub connection: ' + err));

    this.disconnectedHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(Configuration.serviceUrls.disconnectedHubConnection)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.disconnectedHubConnection
      .start()
      .then(() => console.log('disconnected hub Connection started'))
      .catch(err => console.log('Error while starting disconnected hub  connection: ' + err));
  }

  public addSendStatusListener = () => {
    this.statusHubConnection.on('sendstatus', (data: CarStatus) => {
      if (this.subscribers && this.subscribers.length) {
        this.subscribers.forEach(s => {
          s.carStatusChanged(data);
        });
      }
      console.log(data);
    });
  }

  public addCarDisconnectedListener = () => {
    this.disconnectedHubConnection.on('vehicledissconnected', (data: CarDisconnected) => {
      if (this.subscribers && this.subscribers.length) {
        this.subscribers.forEach(s => {
          s.carDisconnectedChanged(data);
        });
      }
      console.log(data);
    });
  }
}
