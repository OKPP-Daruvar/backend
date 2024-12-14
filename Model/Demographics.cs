using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Demographics
    {
        [FirestoreProperty("age")]
        public int Age { get; set; }

        [FirestoreProperty("gender")]
        public string Gender { get; set; }

        [FirestoreProperty("education_level")]
        public string EducationLevel { get; set; }
    }

}
