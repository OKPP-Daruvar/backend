﻿using FirebaseAdmin.Auth;
using Forms.Model;
using Forms.Repository.Config;
using Google.Cloud.Firestore;


namespace Forms.Repository.Survey
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IFirebaseConfig firebaseConfig;

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


            survey.Id = documentRef.Id;
            await documentRef.SetAsync(survey);

            return documentRef.Id;
        }

        public async Task<bool> DeleteSurvey(String surveyId, FirebaseToken firebaseToken)
        {
            try
            {
                firebaseConfig.Initialize();
                var db = firebaseConfig.GetFirestoreDb();
                var userId = firebaseToken.Uid;

                var documentRef = db.Collection("surveys").Document(surveyId);
                DocumentSnapshot snapshot = await documentRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    Console.WriteLine("Document not found.");
                    return false;

                }

                var survey = snapshot.ConvertTo<Model.Survey>();

                if (survey.UserId != userId)
                {
                    Console.WriteLine("User does not own this survey.");
                    return false;

                }

                await documentRef.DeleteAsync();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine($"Unexpected error. {ex.ToString()}");
                return false;
            }

        }

        public async Task<List<Model.Survey>> GetSurveys(FirebaseToken firebaseToken)
        {
            firebaseConfig.Initialize();
            var db = firebaseConfig.GetFirestoreDb();
            var userId = firebaseToken.Uid;

            var surveyQuery = db.Collection("surveys")
                .WhereEqualTo("userId", userId);

            var querySnapshot = await surveyQuery.GetSnapshotAsync();

            var surveys = querySnapshot.Documents
                .Select(doc => doc.ConvertTo<Model.Survey>())
                .ToList();

            return surveys;
            
        }

        public async Task<Model.Survey> GetSurvey(string surveyId)
        {
            firebaseConfig.Initialize();
            var db = firebaseConfig.GetFirestoreDb();

            var surveyDocument = db.Collection("surveys").Document(surveyId);

            var snapshot = await surveyDocument.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                throw new Exception($"Survey with ID {surveyId} does not exist.");
            }

            var survey = snapshot.ConvertTo<Model.Survey>();
            return survey;
        }


        public async Task<bool> SendAnswerAsync(string surveyId, AnswerPost answerPost)
        {
            firebaseConfig.Initialize();
            var db = firebaseConfig.GetFirestoreDb();

            Answer answer = new()
            {
                SubmittedAt = DateTime.UtcNow,
                Answers = answerPost.Answers,
                Respondent = answerPost.Respondent
            };

            try
            {
                var answersCollection = db.Collection("surveys").Document(surveyId).Collection("answers");

                await answersCollection.AddAsync(answer);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add answer: {ex.Message}");
                return false;
            }
        }

    }
}
