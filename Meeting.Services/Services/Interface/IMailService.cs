using Meeting.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services.Interface
{
    public interface IMailService
    {
        void Send(MailAddress from, MailAddress to, string body, string subject, string bcc = "",
            params MailAddress[] cc);

        void SendErrorDeveloper(string body, string subject, string bcc = "",
          params MailAddress[] cc);
    }
}
