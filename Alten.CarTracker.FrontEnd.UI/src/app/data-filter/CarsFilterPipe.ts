import { Pipe, PipeTransform } from '@angular/core';
import { Car } from '../models/Car';
import { StatusLookup } from '../models/StatusLookup';
import { Customer } from '../models/Customer';

@Pipe({ name: 'carsFilter' })

export class CarsFilterPipe implements PipeTransform {
    transform(cars: Car[], status: StatusLookup, customer: Customer) {
        const filted = cars.filter(car => {
            if (status && customer) {
                return car.lastStatusId === status.id && car.customerId === customer.id;
            } else if (status && !customer) {
                return car.lastStatusId === status.id;
            } else if (!status && customer) {
                return car.customerId === customer.id;
            } else { return true; }
        });
        return filted;
    }
}
