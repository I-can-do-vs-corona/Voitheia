import { MinimalUserDTO } from './minimalUserDTO';

export class UpdateUserDTO extends MinimalUserDTO {
    lastName: string;
    street: string;
    zip: string;
    city: string;
    country: string;

    constructor() {
        super();

        this.lastName = "";
        this.street = "";
        this.zip = "";
        this.city = "";
        this.country= "Deutschland";
    }
}