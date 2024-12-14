using Forms.Model.GraphData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Repository.Analytics
{
    public interface IAnalyticsRepository
    {
        Task<List<ChoiceGraphData>> GetGraphDataAsync(string surveyId);
        //Task<List<Answer>> GetAnswersAsync();
    }
}
