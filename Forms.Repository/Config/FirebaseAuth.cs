using Forms.Repository.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Forms.Repository.Config
{
    public class FirebaseAuth : Attribute, IAuthorizationFilter
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

                var decodedToken = await firebaseAuthRepository.VerifyTokenAsync(token);

                context.HttpContext.Items["FirebaseUser"] = decodedToken;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
