using Forms.Repository.Auth;
using Forms.WebApi.Config;
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

        //[FirebaseAuth]
        [Route("RegisterUserAsync")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserModel registerUserModel)
        {
            try
            {
                //string? token = HttpContext.Items["Token"] as String;
                //var firebaseUser = await firebaseAuthRepository.VerifyTokenAsync(token);
                await firebaseAuthRepository.RegisterAsync(registerUserModel.Email, registerUserModel.Password);
                
                return Ok("User registered successfully!");

            }catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
