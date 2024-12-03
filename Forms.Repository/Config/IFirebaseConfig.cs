using Google.Cloud.Firestore;

namespace Forms.Repository.Config
{
    public interface IFirebaseConfig
    {
        void Initialize();
        FirestoreDb GetFirestoreDb();
    }
}
