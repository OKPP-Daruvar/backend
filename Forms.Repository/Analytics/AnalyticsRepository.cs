using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms.Model;
using Google.Cloud.Firestore;
using static Google.Api.FieldInfo.Types;

namespace Forms.Repository.Analytics
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        // TODO - add Class for getting FirebaseDB instance in common layer, use it everywhere where DB access is needed
        FirestoreDb db = FirestoreDb.Create("forms-okpp");

        //public Task<List<Answer>> GetAnswersAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<Answer>> GetAnswersAsync()
        {
            DocumentReference docRef = db.Collection("surveys").Document("survey1");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Console.WriteLine("Document data for {0} document:", snapshot.Id);
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }
            else
            {
                Console.WriteLine("Document {0} does not exist!", snapshot.Id);
            }

            return new List<Answer>();
        }
    }
}
