using Forms.Model.Config;
using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Question
    {
        [FirestoreProperty("questionId")]
        public string QuestionId { get; set; }

        [FirestoreProperty("text")]
        public string Text { get; set; }

        [FirestoreProperty(ConverterType = typeof(EnumConverter<QuestionType>))]
        public QuestionType Type { get; set; }

        [FirestoreProperty("options")]
        public List<string> Options { get; set; }
    }

}
