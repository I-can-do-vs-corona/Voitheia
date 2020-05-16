export class SetNewEmailDTO {
    oldEmail: string;
    newEmail: string;
    newEmailConfirm: string;
    
    constructor(){ 
        this.oldEmail = "";
        this.newEmail = "";
        this.newEmailConfirm = "";
    }
}