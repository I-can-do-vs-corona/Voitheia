namespace ActiveCruzer.Models
{
    public class RequestDoesNotExistError :ErrorModel
    {
        ///<example>3</example>
        public override int Code => ErrorCodes.RequestDoesNotExist;
        ///<example>This request does not exist</example>
        public override string Errormessage => "This request does not exist";
    }
}