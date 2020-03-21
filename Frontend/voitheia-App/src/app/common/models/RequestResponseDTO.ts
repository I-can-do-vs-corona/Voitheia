import { RequestDTO } from './requestDTO';

export class RequestResponseDTO extends RequestDTO {
    public distanceToUser: number;
    public id: number;

    constructor() {
        super();
        this.distanceToUser = 0;
        this.id = 0;
    }
}