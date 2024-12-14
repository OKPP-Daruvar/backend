using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Repository.Analytics
{
    public class AnalyticsFilter
    {
        public AnalyticsFilter() {
            SurveyId = "";
            MinAge = null;
            MaxAge = null;
            Sex = "";
            EducationLevel = "";
        }

        public AnalyticsFilter(string surveyId, int? minAge, int? maxAge, string? sex, string? educationLevel)
        {
            SurveyId = surveyId;
            MinAge = minAge;
            MaxAge = maxAge;
            Sex = sex;
            EducationLevel = educationLevel;
        }

        public string SurveyId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }

        public string? Sex { get; set; }

        public string? EducationLevel { get; set; }
    }
}
