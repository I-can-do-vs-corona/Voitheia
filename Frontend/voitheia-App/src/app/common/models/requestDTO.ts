
export class RequestDTO {
    public type: string;
    public lastName: string;
    public firstName: string;
    public email: string;
    public phoneNumber: string;
    public street: string;
    public zip: string;
    public city: string;
    public description: string;

    constructor() {
        this.type = "";
        this.lastName = "";
        this.firstName = "";
        this.email = "";
        this.phoneNumber = "";
        this.street = "";
        this.zip = "";
        this.city = "";
        this.description = "";
    }
}