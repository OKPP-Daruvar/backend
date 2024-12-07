using System;
using Forms.Repository.Auth;
using Forms.WebApi.Config;
using Microsoft.AspNetCore.Mvc;
using Forms.Repository.Analytics;

namespace Forms.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        IAnalyticsRepository repository { get; set; }   
        public AnalyticsController(IAnalyticsRepository analyticsRepository)
        {
            this.repository = analyticsRepository;
        }


        [HttpGet]
        public string GetAnswersAsync()
        {
            repository.GetAnswersAsync();
            return "";
        }
    }
}

