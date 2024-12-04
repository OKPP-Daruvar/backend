using Forms.Repository.EmailService;
using Forms.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]

public class EmailServiceController : ControllerBase
    {
        IEmailServiceRepository _emailServiceRepository;
        IQRCodeServiceRepository _qrCodeServiceRepository;
        public EmailServiceController(IEmailServiceRepository emailServiceRepository, IQRCodeServiceRepository qRCodeServiceRepository)
        {
            _emailServiceRepository = emailServiceRepository;
            _qrCodeServiceRepository = qRCodeServiceRepository;
        }

    [Route("SendEmail")]
    [HttpPost]
        public async Task<HttpResponseMessage> SendEmail([FromBody] EmailRequest emailRequest)
        {
        var response = new HttpResponseMessage();
        
        try
        {
            var qrCodeBytes = _qrCodeServiceRepository.GenerateQRCode(emailRequest.SurveyLink);

            foreach (var email in emailRequest.Emails)
            {
                await _emailServiceRepository.SendEmailAsync(
                                email,
                                "Your link to access a survey",
                                $"Click here to access the survey: {emailRequest.SurveyLink} or scan the attached QR code.",
                                qrCodeBytes,
                                "survey_qr_code.png"
                            );
            }

            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent("Emails sent successfully");

        }
        catch (Exception ex) { 
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Content = new StringContent($"Error: {ex.Message}");
        
        }

        return response;
            
        }

    }

