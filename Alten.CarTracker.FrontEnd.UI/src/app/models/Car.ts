import { CarStatus } from './carStatus';

export class Car {
    public vin: string;
    public registrationNumber: string;
    public customerId: number;
    public carStatuses: Array<CarStatus> = new Array<CarStatus>();
    public lastStatusId: Number;
}
