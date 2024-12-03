﻿using FirebaseAdmin.Auth;
using Forms.Model;

namespace Forms.Repository.Survey
{
    public interface ISurveyRepository
    {
        Task<string> CreateSurvey(SurveyPost survey, FirebaseToken firebaseToken);
    }
}