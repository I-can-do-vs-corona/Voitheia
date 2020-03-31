import { environment } from 'src/environments/environment';

export class LoginCredentials {
    email: string;
    password: string;
    minutesValid: number;
    
    constructor(email: string, password: string){ 
        this.email = email;
        this.password = password;
        this.minutesValid = environment.defaultSessionLifetimeMinutes;
    }
}