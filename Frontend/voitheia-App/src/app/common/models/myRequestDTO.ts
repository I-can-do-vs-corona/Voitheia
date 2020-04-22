import { MinimalUserDTO } from './minimalUserDTO';
import { RequestResponseDTO } from './requestResponseDTO';

export class MyRequestDTO extends RequestResponseDTO {
    public assignedUser: MinimalUserDTO;
    public author: boolean;

    constructor() {
        super();
        
        this.assignedUser = new MinimalUserDTO();
        this.author = true;
    }
}