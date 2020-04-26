namespace ActiveCruzer.Models.Error
{
    public class NotAuthorizedToPerformActionError : ErrorModel
    {
        public override int Code => ErrorCodes.NotAuthorizedToPerformAction;
        ///<example>You are not allowed to perform this action.</example>
        public override string Errormessage => "You are not authorized to perform this action.";
    }
}