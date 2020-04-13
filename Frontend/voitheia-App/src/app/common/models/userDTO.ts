import { UpdateUserDTO } from './updateUserDTO';
import * as moment from 'moment';

export class UserDTO extends UpdateUserDTO  {
    email: string;
    registerDate: Date;

    constructor() {
        super();

        this.email = "";
        //var now = new Date;
        this.registerDate = new Date(localStorage.getItem("validUntil"));
    }
}