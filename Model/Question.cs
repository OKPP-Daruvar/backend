using Forms.Model.Config;
using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Question
    {
        [FirestoreProperty]
        public string QuestionId { get; set; }

        [FirestoreProperty]
        public string Text { get; set; }

        [FirestoreProperty(ConverterType = typeof(EnumConverter<QuestionType>))]
        public QuestionType Type { get; set; }

        [FirestoreProperty]
        public List<string> Options { get; set; }
    }

}
