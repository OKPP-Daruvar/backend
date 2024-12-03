using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Survey
    {

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string Title { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public List<Question> Questions { get; set; }

        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
    }

}
