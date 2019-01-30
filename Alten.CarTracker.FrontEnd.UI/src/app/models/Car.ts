import { CarStatus } from './carStatus';

export class Car {
    vin: string;
    registrationNumber: string;
    customerId: number;
    carStatuses: Array<CarStatus>;
    status: string;
}
