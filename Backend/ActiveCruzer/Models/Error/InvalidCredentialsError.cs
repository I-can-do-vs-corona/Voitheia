namespace ActiveCruzer.Models.Error
{
    public class InvalidCredentialsError : ErrorModel
    {
        ///<example>9</example>
        public override int Code => ErrorCodes.InvalidCredentials;

        ///<example>Your credentials are invalid</example>
        public override string Errormessage => "Your credentials are invalid";
    }
}