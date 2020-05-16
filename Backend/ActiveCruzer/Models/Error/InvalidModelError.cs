namespace ActiveCruzer.Models.Error
{
    public class InvalidModelError : ErrorModel
    {
        ///<example>11</example>
        public override int Code => ErrorCodes.InvalidModel;

        ///<example>invalid model</example>
        public override string Errormessage => "invalid model";
    }
}