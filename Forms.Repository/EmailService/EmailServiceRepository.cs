using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Forms.Model;
using Microsoft.Extensions.Configuration;
namespace Forms.Repository.EmailService
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        private readonly string _sendGridApiKey;

        public EmailServiceRepository(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGrid:ApiKey"];
        }

        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body, byte[] attachmentBytes, string attachmentFileName)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("formsurveyapp@gmail.com", "Forms");
            var to = new EmailAddress(recipientEmail);
            var message = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var attachment = Convert.ToBase64String(attachmentBytes);
            message.AddAttachment(attachmentFileName, attachment);
            var response = await client.SendEmailAsync(message);

            if (response != null)
            {
                return true;
            }
            else
            {
                throw new Exception($"Failed to send email. Status Code: {response?.StatusCode}");
            }
        }
    }

    
}
