﻿using Google.Cloud.Firestore;

namespace Forms.Model
{
    [FirestoreData]
    public class Answer
    {
        [FirestoreProperty]
        public Demographics Respondent { get; set; }

        [FirestoreProperty]
        public Dictionary<string, List<string>> Answers { get; set; } // (Key = QuestionId, Value = list<string>)

        [FirestoreProperty]
        public DateTime SubmittedAt { get; set; } 
    }

}
