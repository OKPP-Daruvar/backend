using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Demographics
    {
        [FirestoreProperty]
        public int Age { get; set; }

        [FirestoreProperty]
        public string Gender { get; set; }

        [FirestoreProperty]
        public string EducationLevel { get; set; }
    }

}
