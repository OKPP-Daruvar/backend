﻿using Forms.Model;
using Forms.Repository.Auth;
using Forms.Repository.Survey;
using Forms.WebApi.Config;
using Microsoft.AspNetCore.Mvc;

namespace Forms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private IFirebaseAuthRepository firebaseAuthRepository;
        private ISurveyRepository surveyRepository;

        public SurveyController(IFirebaseAuthRepository firebaseAuthRepository, ISurveyRepository surveyRepository)
        {
            this.firebaseAuthRepository = firebaseAuthRepository;
            this.surveyRepository = surveyRepository;
        }

        /// <summary>
        /// Creates a new Survey.
        /// </summary>
        /// <param name="surveyPost">The survey details to be created.</param>
        /// <returns>If successful, returns the survey ID.</returns>
        [FirebaseAuth]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SurveyPost surveyPost)
        {
            string surveyId = "";
            try
            {
                string? token = HttpContext.Items["Token"] as String;
                if (token == null)
                {
                    return StatusCode(401, token);
                }
                var firebaseUser = await firebaseAuthRepository.VerifyTokenAsync(token);

                surveyId = await surveyRepository.CreateSurvey(surveyPost, firebaseUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

            return Ok(surveyId);
        }
    }
}