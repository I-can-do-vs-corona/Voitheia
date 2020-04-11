import { UpdateUserDTO } from './updateUserDTO';

export class UserDTO extends UpdateUserDTO  {
    email: string;

    constructor() {
        super();

        this.email = "";
    }
}