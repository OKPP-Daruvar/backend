using Forms.Repository.Auth;
using Microsoft.AspNetCore.Mvc;
using Model.Auth;

namespace Forms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IFirebaseAuthRepository firebaseAuthRepository;

        public AuthController(IFirebaseAuthRepository firebaseAuthRepository)
        {
            this.firebaseAuthRepository = firebaseAuthRepository;
        }

        [Route("VerifyTokenAsync")]
        [HttpGet]
        public async Task<IActionResult> VerifyTokenAsync(string token)
        {
            return Ok(await firebaseAuthRepository.VerifyTokenAsync(token));
        }


        [Route("RegisterUserAsync")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserModel registerUserModel)
        {
            try
            {
                await firebaseAuthRepository.RegisterAsync(registerUserModel.Email, registerUserModel.Password);

                return Ok("User registered successfully!");

            }catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
