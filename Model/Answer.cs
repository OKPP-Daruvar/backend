using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Answer
    {
        [FirestoreProperty]
        public Demographics Respondent { get; set; }

        [FirestoreProperty]
        public Dictionary<string, object> Answers { get; set; } // (Key = QuestionId, Value = (string/list<T>))

        [FirestoreProperty]
        public DateTime SubmittedAt { get; set; } 
    }

}
