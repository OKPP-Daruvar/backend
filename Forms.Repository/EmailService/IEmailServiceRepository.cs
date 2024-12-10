using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms.Model;
namespace Forms.Repository.EmailService
{
    public interface IEmailServiceRepository
    {
        Task<bool> SendEmailAsync(string recipientEmail, string subject, string body, byte[] attachmentBytes, string attachmentFileName);
    }
}
