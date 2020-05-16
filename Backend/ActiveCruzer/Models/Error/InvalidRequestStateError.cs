namespace ActiveCruzer.Models
{
    public class InvalidRequestStateError : ErrorModel
    {
        /// <example>4</example>
        public override int Code => ErrorCodes.InvalidRequestState;
        ///<example>"The request is in an invalid state"</example>
        public override string Errormessage => "The request is in an invalid state";
    }
}