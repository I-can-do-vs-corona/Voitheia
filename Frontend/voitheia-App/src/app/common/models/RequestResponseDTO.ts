import { RequestDTO } from './requestDTO';

export class RequestResponseDTO extends RequestDTO {
    public distanceToUser: number;

    constructor() {
        super();
        this.distanceToUser = 0;
    }
}