import { RequestDTO } from './requestDTO';

export class RequestResponseDTO extends RequestDTO {
    public distanceToUser: number;
    public id: number;
    public status: string;

    constructor() {
        super();
        this.distanceToUser = 0;
        this.id = 0;
        this.status = "";
    }
}