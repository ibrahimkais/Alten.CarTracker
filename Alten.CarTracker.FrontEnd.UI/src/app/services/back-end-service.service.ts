import { Injectable } from '@angular/core';
import configuration from '../Data/configurations.json';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/Customer.js';
import { Car } from '../models/Car.js';
import { StatusLookup } from '../models/StatusLookup.js';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BackEndService {

  constructor(private http: HttpClient) { }

  getCustomers = (): Observable<Customer[]> => this.http.get<Customer[]>(configuration.serviceUrls.getCustomers);

  getCars = (): Observable<Car[]> => this.http.get<Car[]>(configuration.serviceUrls.getCars);

  getCarsStatsesLookup = (): Observable<StatusLookup[]> => this.http.get<StatusLookup[]>(configuration.serviceUrls.getStatuses);
}
