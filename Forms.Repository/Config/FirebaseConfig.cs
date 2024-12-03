using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace Forms.Repository.Config
{
    public class FirebaseConfig : IFirebaseConfig
    {
        private FirestoreDb firestoreDb;

        public void Initialize()
        {
            try
            {
                string serviceAccountJson = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_JSON");
                string serviceAccountJsonPath = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_PATH");

                GoogleCredential credential = null;

                if (!string.IsNullOrEmpty(serviceAccountJson))
                {
                    credential = GoogleCredential.FromJson(serviceAccountJson);
                    FirebaseApp.Create(new AppOptions { Credential = credential });
                }
                else if (!string.IsNullOrEmpty(serviceAccountJsonPath))
                {
                    credential = GoogleCredential.FromFile(serviceAccountJsonPath);
                    FirebaseApp.Create(new AppOptions { Credential = credential });
                }
                else
                {
                    throw new InvalidOperationException("Firebase service account credentials are missing or incorrect.");
                }

                // Kreiranje Firestore klijenta koristeći GoogleCredential
                FirestoreClientBuilder clientBuilder = new FirestoreClientBuilder
                {
                    Credential = credential
                };
                var firestoreClient = clientBuilder.Build();

                firestoreDb = FirestoreDb.Create("forms-okpp", firestoreClient);
                Console.WriteLine("Firestore connection established.");

                Console.WriteLine("Firebase initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Firebase initialization failed: {ex.Message}");
            }
        }

        public FirestoreDb GetFirestoreDb()
        {
            if (firestoreDb == null)
            {
                throw new InvalidOperationException("FirestoreDb is not initialized. Call Initialize() first.");
            }
            return firestoreDb;
        }
    }
}
