namespace ActiveCruzer.Models
{
    public class InvalidAddressError : ErrorModel
    {
        ///<example>5</example>
        public override int Code => ErrorCodes.InvalidAddress;

        ///<example>The provided adress is not valid. Please check for the spelling of the street. Accepted: Sankt-Boni. Invalid: St.-Boni.</example>
        public override string Errormessage =>
            "The provided adress is not valid. Please check for the spelling of the street. Accepted: Sankt-Boni. Invalid: St.-Boni.";
    }
}