namespace ActiveCruzer.Models
{
    public class TryAgainLaterError: ErrorModel
    {
        ///<example>10</example>
        public override int Code => ErrorCodes.TryAgain;
        ///<example>An error occured. Please try again later</example>
        public override string Errormessage => "An error occured. Please try again later";
    }
}