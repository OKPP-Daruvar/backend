using System;
using Forms.Repository.Auth;
using Forms.WebApi.Config;
using Microsoft.AspNetCore.Mvc;
using Forms.Repository.Analytics;
using Forms.Model;

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
        public Task<List<GraphData>> GetAnswersAsync(string surveyId)
        {
            Task<List<GraphData>> graphDataList = repository.GetGraphDataAsync(surveyId);
            return graphDataList;
        }
    }
}

