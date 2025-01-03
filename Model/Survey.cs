using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Survey
    {

        [FirestoreProperty("userId")]
        public string UserId { get; set; }

        [FirestoreProperty("id")]
        public string Id { get; set; }

        [FirestoreProperty("title")]
        public string Title { get; set; }

        [FirestoreProperty("description")]
        public string Description { get; set; }

        [FirestoreProperty("questions")]
        public List<Question> Questions { get; set; }

        [FirestoreProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

}
