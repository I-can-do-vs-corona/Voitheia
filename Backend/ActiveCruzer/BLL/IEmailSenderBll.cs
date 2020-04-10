using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.BLL
{
    public interface IEmailSenderBll : IDisposable
    {
        Task SendEmailAsync(string firstname, string email, string message);
    }
}
