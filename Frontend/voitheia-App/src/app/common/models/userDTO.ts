import { UpdateUserDTO } from './updateUserDTO';
import * as moment from 'moment';

export class UserDTO extends UpdateUserDTO  {
    createdOn: Date;
    lastLogin: Date;
    emailConfirmed: boolean;

    constructor() {
        super();

        this.createdOn = new Date();
        this.lastLogin = new Date();
        this.emailConfirmed = true;
    }
}