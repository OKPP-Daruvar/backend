using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;

namespace Forms.Repository.Config
{
    public class FirebaseConfig : IFirebaseConfig
    {
        public void Initialize()
        {
            try
            {
                string serviceAccountJson = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_JSON");
                string serviceAccountJsonPath = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_PATH");

                if (!string.IsNullOrEmpty(serviceAccountJson))
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromJson(serviceAccountJson)
                    });
                }
                else if(serviceAccountJsonPath != null)
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromFile(serviceAccountJsonPath)
                    });
                }
                else
                {
                    throw new InvalidOperationException("Firebase service account credentials are missing or incorrect.");
                }

                Console.WriteLine("Firebase initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Firebase initialization failed: {ex.Message}");
            }
        }
    }
}
