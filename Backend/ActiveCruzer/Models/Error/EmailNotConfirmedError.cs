namespace ActiveCruzer.Models
{
    public class EmailNotConfirmedError : ErrorModel
    {
        /// <example>1</example>
        public override int Code => ErrorCodes.EmailNotConfirmed;
        /// <example>"Your email address is not verified"</example>
        public override string Errormessage => "Your email address is not verified";
    }
}