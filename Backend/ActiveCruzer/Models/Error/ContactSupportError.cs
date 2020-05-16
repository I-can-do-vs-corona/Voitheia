namespace ActiveCruzer.Models.Error
{
    public class ContactSupportError : ErrorModel
    {
        ///<example>7</example>
        public override int Code => ErrorCodes.ContactSupport;

        ///<example>An error occured. Please contact the support</example>
        public override string Errormessage => "An error occured. Please contact the support";
    }
}