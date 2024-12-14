using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Forms.Model;
using Forms.Model.GraphData;
using Forms.Repository.Config;
using Google.Cloud.Firestore;
using static Google.Api.FieldInfo.Types;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace Forms.Repository.Analytics
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private IFirebaseConfig firebaseConfig;

        public AnalyticsRepository(IFirebaseConfig firebaseConfig)
        {
            this.firebaseConfig = firebaseConfig;
        }

        public async Task<List<GraphData>> GetGraphDataAsync(AnalyticsFilter filter)
        {
            var graphDataList = new List<GraphData>();

            firebaseConfig.Initialize();
            var db = firebaseConfig.GetFirestoreDb();

            //var userId = firebaseToken.Uid;

            DocumentReference surveyDocReference = db.Collection("surveys").Document(filter.SurveyId);

            DocumentSnapshot surveySnapshot = await surveyDocReference.GetSnapshotAsync();
            Model.Survey survey = surveySnapshot.ConvertTo<Model.Survey>();

            Query answersQuery = BuildFilterQuery(surveyDocReference, filter);
            QuerySnapshot answersSnapshot = await answersQuery.GetSnapshotAsync();

            //QuerySnapshot answersSnapshot = await surveyDocReference.Collection("answers").GetSnapshotAsync();
            var answers = new List<Answer>();
            foreach (DocumentSnapshot answerDoc in answersSnapshot.Documents)
            {
                if (answerDoc.Exists)
                {
                    Answer answer = answerDoc.ConvertTo<Answer>();
                    answers.Add(answer);
                }
            }

            var questions = survey.Questions;

            foreach (var question in questions)
            {
                if (question.Type != QuestionType.OpenText)
                {
                    ChoiceGraphData choiceGraphData = new ChoiceGraphData();
                    choiceGraphData.Id = question.QuestionId;
                    choiceGraphData.Question = question;
                    Dictionary<string, int> choiceAnswers = new Dictionary<string, int>();

                    foreach (var answerOption in question.Options)
                    {
                        choiceAnswers[answerOption] = 0;
                    }

                    foreach (var answer in answers)
                    {
                        if (answer.Answers.TryGetValue(question.QuestionId, out var questionAnswerChoices))
                        {
                            foreach (var questionAnswerChoice in questionAnswerChoices)
                            {
                                choiceAnswers[questionAnswerChoice] += 1;
                            }
                        }
                    }

                    choiceGraphData.Answers = choiceAnswers;

                    graphDataList.Add(choiceGraphData);

                } else {
                    OpenTextGraphData openTextGraphData = new OpenTextGraphData();
                    openTextGraphData.Id = question.QuestionId;
                    openTextGraphData.Question = question;
                    List<string>? openTextAnswers = new List<string>();

                    foreach (var answer in answers)
                    {
                        if (answer.Answers.TryGetValue(question.QuestionId, out var questionAnswer))
                        {
                            // OpenTextAnswers are stored in a list with one element, therefore to access answers [0] is used
                            openTextAnswers.Add(questionAnswer[0]);
                        }
                    }
                    openTextGraphData.Answers = openTextAnswers;

                    graphDataList.Add(openTextGraphData);
                }
            }

            return graphDataList;
        }

        private Query BuildFilterQuery(DocumentReference surveyDocReference, AnalyticsFilter filter)
        {
            Query query = surveyDocReference.Collection("answers");

            if (!string.IsNullOrEmpty(filter.Sex))
            {
                query = query.WhereEqualTo("respondent.gender", filter.Sex);
            }

            if (filter.MinAge != null)
            {
                query = query.WhereGreaterThanOrEqualTo("respondent.age", filter.MinAge);
            }

            if (filter.MaxAge != null)
            {
                query = query.WhereLessThanOrEqualTo("respondent.age", filter.MaxAge);
            }

            if (!string.IsNullOrEmpty(filter.EducationLevel))
            {
                query = query.WhereEqualTo("respondent.education_level", filter.EducationLevel);
            }

            return query;
        }

        private List<Answer> GetAnswers(string questionId)
        {
            // TODO - async method?
            return new List<Answer>();
        }
    }
}
