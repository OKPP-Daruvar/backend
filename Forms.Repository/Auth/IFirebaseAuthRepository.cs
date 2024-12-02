using FirebaseAdmin.Auth;

namespace Forms.Repository.Auth
{
    public interface IFirebaseAuthRepository
    {
        Task<bool> RegisterAsync(string username, string password);
        Task<FirebaseToken> VerifyTokenAsync(string token);
    }
}
