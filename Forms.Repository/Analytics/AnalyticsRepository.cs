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

namespace Forms.Repository.Analytics
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private IFirebaseConfig firebaseConfig;

        public AnalyticsRepository(IFirebaseConfig firebaseConfig)
        {
            this.firebaseConfig = firebaseConfig;
        }

        public async Task<List<GraphData>> GetGraphDataAsync(string surveyId)
        {
            var graphDataList = new List<GraphData>();

            firebaseConfig.Initialize();

            //var userId = firebaseToken.Uid;

            var db = firebaseConfig.GetFirestoreDb();

            //answersSnapshot = await db.Collection("surveys").Document(surveyId).Collection("answers").GetSnapshotAsync();

            DocumentReference surveyDocReference = db.Collection("surveys").Document(surveyId);

            DocumentSnapshot surveySnapshot = await surveyDocReference.GetSnapshotAsync();

            Model.Survey survey = surveySnapshot.ConvertTo<Model.Survey>();



            QuerySnapshot answersSnapshot = await surveyDocReference.Collection("answers").GetSnapshotAsync();

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
                Dictionary<string, int> choiceAnswers = new Dictionary<string, int>();
                List<string> openTextAnswers = new List<string>();
                if (question.Type != QuestionType.OpenText)
                {
                    foreach (var answerOption in question.Options)
                    {
                        choiceAnswers[answerOption] = 0;
                    }


                } else
                {
                    foreach (var answer in answers)
                    {
                        if (answer.Answers.TryGetValue(question.QuestionId, out var questionAnswer))
                        {
                            openTextAnswers.Add(questionAnswer);
                        }
                        choiceAnswers.Add(answer.Answers[question.QuestionId] as Answer);
                    }
                }
                
                // TODO - if no options then put answers in a list, othewise put them in dictionary answer: answerCount

                if (question != null)
                {
                    ChoiceGraphData graphData = new ChoiceGraphData();
                    graphData.Question = question;

                    foreach (var answer in answers)
                    {
                        //if(answer.Answers.TryGetValue(question.QuestionId, out var questionAnswer))
                        //{
                        //    questionAnswers[questionAnswer] = questionAnswers[questionAnswer] + 1;
                        //}
                        //questionAnswers.Add(answer.Answers[question.QuestionId] as Answer);
                    }

                    graphData.Answers = questionAnswers;
                    graphDataList.Add(graphData);
                }

            }

            return graphDataList;
        }

        private List<Answer> GetAnswers(string questionId)
        {
            // TODO - async method?
            return new List<Answer>();
        }
    }
}
