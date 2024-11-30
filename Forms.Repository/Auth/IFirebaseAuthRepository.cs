using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Repository.Auth
{
    public interface IFirebaseAuthRepository
    {
        Task<bool> RegisterAsync(string username, string password);
        Task<FirebaseToken> VerifyTokenAsync(string token);
    }
}
