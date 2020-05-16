export class MinimalUserDTO {
    firstName: string;
    phoneNumber: string;
    email: string;
    profilePicture: string;

    constructor() {
        this.firstName = "";
        this.phoneNumber = "";
        this.email = "";
        this.profilePicture = "";
    }
}