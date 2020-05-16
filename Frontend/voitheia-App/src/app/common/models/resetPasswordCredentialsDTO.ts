export class ResetPasswordCredentials {
    public token: string;
    public email: string;
    public password: string;
    public confirmPassword: string;

    constructor() {
        this.token = '';
        this.email = '';
        this.password = '';
        this.confirmPassword = '';
    }
}