using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Answer
    {
        [FirestoreProperty("respondent")]
        public Demographics? Respondent { get; set; }

        [FirestoreProperty("answers")]
        public Dictionary<string, List<string>> Answers { get; set; } // (Key = QuestionId, Value = list<string>)

        [FirestoreProperty("submitted_at")]
        public DateTime SubmittedAt { get; set; } 
    }

}
