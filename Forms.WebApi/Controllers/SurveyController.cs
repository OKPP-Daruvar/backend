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
        /// Sends answer from a user.
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="answerPost"></param>
        /// <returns></returns>
        [Route("SendAnswer")]
        [HttpPost]
        public async Task<IActionResult> SendAnswerAsync(string surveyId, [FromBody] AnswerPost answerPost)
        {
            bool status;
            try
            {
                status = await surveyRepository.SendAnswerAsync(surveyId, answerPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
            if (status) { return Ok(); }
            else { return BadRequest(); }
        }

        /// <summary>
        /// Creates a new Survey.
        /// </summary>
        /// <param name="surveyPost">The survey details to be created.</param>
        /// <returns>If successful, returns the survey ID.</returns>
        [Route("CreateSurvey")]
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

        /// <summary>
        /// Gets a survey by id.
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [Route("GetSurvey")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyAsync(string surveyId)
        {
            Survey res;
            try
            {
                res = await surveyRepository.GetSurvey(surveyId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

            return Ok(res);
        }

        /// <summary>
        /// Gets surveys.
        /// </summary>
        /// <returns></returns>
        [FirebaseAuth]
        [Route("GetSurveys")]
        [HttpGet]
        public async Task<IActionResult> GetSurveysAsync()
        {
            try
            {
                string? token = HttpContext.Items["Token"] as String;
                if (token == null)
                {
                    return StatusCode(401, token);
                }
                var firebaseUser = await firebaseAuthRepository.VerifyTokenAsync(token);

                List<Survey> surveys = await surveyRepository.GetSurveys(firebaseUser);

                return Ok(surveys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes survey.
        /// </summary>
        /// <param name="surveyId"></param>
        [FirebaseAuth]
        [Route("DeleteSurvey")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string surveyId)
        {
            try
            {
                string? token = HttpContext.Items["Token"] as String;
                if (token == null)
                {
                    return StatusCode(401, token);
                }
                var firebaseUser = await firebaseAuthRepository.VerifyTokenAsync(token);

                await surveyRepository.DeleteSurvey(surveyId, firebaseUser);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
            return Ok(surveyId);

        }
    }
}
