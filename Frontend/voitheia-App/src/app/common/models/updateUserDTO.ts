export class UpdateUserDTO {
    email: string;
    firstName: string;
    lastName: string;
    street: string;
    zip: string;
    city: string;
    country: string;

    constructor() {
        this.email = "";
        this.firstName = "";
        this.lastName = "";
        this.street = "";
        this.zip = "";
        this.zip = "";
        this.city = "";
        this.country= "Deutschland";
    }
}