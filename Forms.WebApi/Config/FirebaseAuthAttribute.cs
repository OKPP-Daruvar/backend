using Forms.Repository.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Forms.WebApi.Config
{
    public class FirebaseAuthAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string firebaseAuthHeader = "Authorization";

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers[firebaseAuthHeader].ToString();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token.Substring(7);
                }

                var firebaseAuthRepository = context.HttpContext.RequestServices.GetRequiredService<IFirebaseAuthRepository>();
                context.HttpContext.Items["Token"] = token;

                var decodedToken = await firebaseAuthRepository.VerifyTokenAsync(token);
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
