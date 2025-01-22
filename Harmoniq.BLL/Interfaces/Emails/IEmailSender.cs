using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.Interfaces.Emails
{
    public interface IEmailSender
    {
        Task SendEmail(string to, string subject, string body);
    }
}