using Forms.Model.GraphData;
using Forms.Repository.Analytics;
using Forms.WebApi.Config;
using Microsoft.AspNetCore.Mvc;

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


        /// <summary>
        /// Get questions and answers data.
        /// </summary>
        [FirebaseAuth]
        [HttpGet]
        public async Task<IActionResult> GetAnswersAsync(string surveyId, int? minAge = null, int? maxAge = null, string? sex = null, string? educationLevel = null)
        {
            List<GraphData> graphDataList = new List<GraphData>();

            AnalyticsFilter filter = new AnalyticsFilter(surveyId, minAge, maxAge, sex, educationLevel);

            try
            {
                graphDataList = await repository.GetGraphDataAsync(filter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            return Ok(graphDataList);

        }
    }
}

