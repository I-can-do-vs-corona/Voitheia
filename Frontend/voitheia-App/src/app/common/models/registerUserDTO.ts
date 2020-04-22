export class RegisterUserDTO {
    email: string;
    firstName: string;
    lastName: string;
    password: string;
    street: string;
    zip: string;
    city: string;
    country: string;
    termsAccepted: string;

    constructor() {
        this.email = "";
        this.firstName = "";
        this.lastName = "";
        this.password = "";
        this.street = "";
        this.zip = "";
        this.city = "";
        this.country = "Deutschland";
        this.termsAccepted = null;
    }
}