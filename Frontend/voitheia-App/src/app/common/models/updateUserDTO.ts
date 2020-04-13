export class UpdateUserDTO {
    firstName: string;
    lastName: string;
    street: string;
    zip: string;
    city: string;
    country: string;

    constructor() {
        this.firstName = "";
        this.lastName = "";
        this.street = "";
        this.zip = "";
        this.city = "";
        this.country= "Deutschland";
    }
}