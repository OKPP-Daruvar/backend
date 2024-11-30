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
                string serviceAccountPath = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_PATH");
                string serviceAccountJson = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_JSON");

                if (!string.IsNullOrEmpty(serviceAccountJson))
                {
                    // If we have the JSON content as an environment variable, use it directly
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromJson(serviceAccountJson)
                    });
                }
                else if (!string.IsNullOrEmpty(serviceAccountPath) && File.Exists(serviceAccountPath))
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromFile(serviceAccountPath)
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
