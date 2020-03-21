import { RequestDTO } from './requestDTO';

export class RequestResponseDTO extends RequestDTO {
    public volunteerUserId: string;

    constructor() {
        super();
        this.volunteerUserId = "";
    }
}