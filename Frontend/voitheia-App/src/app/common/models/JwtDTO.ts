import { environment } from 'src/environments/environment';

export class JwtDTO {
    token: string;
    validUntil: Date;
    
    constructor(token: string, validUntil: Date){ 
        this.token = token;
        this.validUntil = validUntil;
    }
}