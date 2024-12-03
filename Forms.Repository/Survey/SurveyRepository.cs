using FirebaseAdmin.Auth;
using Forms.Model;
using Forms.Repository.Config;


namespace Forms.Repository.Survey
{
    public class SurveyRepository : ISurveyRepository
    {
        private IFirebaseConfig firebaseConfig;

        public SurveyRepository(IFirebaseConfig firebaseConfig)
        {
            this.firebaseConfig = firebaseConfig;
        }

        public async Task<string> CreateSurvey(SurveyPost surveyPost, FirebaseToken firebaseToken)
        {
            firebaseConfig.Initialize();

            var userId = firebaseToken.Uid;

            var survey = new Model.Survey
            {
                UserId = userId,
                Title = surveyPost.Title,
                Description = surveyPost.Description,
                CreatedAt = DateTime.UtcNow,
                Questions = surveyPost.Questions.Select((question, index) => new Question
                {
                    QuestionId = $"q{index + 1}",
                    Text = question.Text,
                    Type = question.Type,
                    Options = question.Options
                }).ToList()
            };

            var db = firebaseConfig.GetFirestoreDb();

            var documentRef = db.Collection("surveys").Document();

            await documentRef.SetAsync(survey);

            return documentRef.Id;
        }
    }
}
