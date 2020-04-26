namespace ActiveCruzer.Models
{
    public class OwnRequestError : ErrorModel
    {
        /// <example>2</example>
        public override int Code => ErrorCodes.OwnRequest;
        /// <example>It is not possible to take your own request</example>
        public override string Errormessage => "It is not possible to take your own request";
    }
}