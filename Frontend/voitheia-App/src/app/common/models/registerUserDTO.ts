import { UserDTO } from './userDTO';

export class RegisterUserDTO extends UserDTO  {
    password: string;

    constructor() {
        super();

        this.password = "";
    }
}