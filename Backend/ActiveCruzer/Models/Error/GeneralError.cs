namespace ActiveCruzer.Models.Error
{
    public class GeneralError : ErrorModel
    {
        public GeneralError(int code, string message)
        {
            Code = code;
            Errormessage = message;
        }
        public override int Code { get; }
        public override string Errormessage { get; }
    }
}