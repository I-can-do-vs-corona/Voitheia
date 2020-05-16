import { environment } from 'src/environments/environment';

export class ConfirmEmailDTO {
    email: string;
    emailToken: string;
    
    constructor(){ 
        this.email = "";
        this.emailToken = "";
    }
}