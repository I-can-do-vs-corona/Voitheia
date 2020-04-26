using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models.Error
{
    public class AccountLockedError : ErrorModel
    {
        /// <example>8</example>
        public override int Code => ErrorCodes.AccountLocked;
        ///<example>Your account is locked. Please try it in 5 minutes again</example>
        public override string Errormessage => "Your account is locked. Please try it in 5 minutes again";
    }
}
