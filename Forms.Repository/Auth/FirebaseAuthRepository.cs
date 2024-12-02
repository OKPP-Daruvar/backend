using FirebaseAdmin.Auth;
using Forms.Repository.Config;
using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;

namespace Forms.Repository.Auth
{
    public class FirebaseAuthRepository : IFirebaseAuthRepository
    {
        private IFirebaseConfig firebaseConfig;

        public FirebaseAuthRepository(IFirebaseConfig firebaseConfig)
        {
            this.firebaseConfig = firebaseConfig;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            UserRecord? user = null;

            try
            {
                firebaseConfig.Initialize();

                user = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
                {
                    Email = email,
                    Password = password
                });
            }catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred during user registration. {ex.Message}", ex);
            }
            return user != null;
        }

        public async Task<FirebaseToken> VerifyTokenAsync(string idToken)
        {
            try
            {
                firebaseConfig.Initialize();

                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

                return decodedToken;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while verifying the ID token. {ex.Message}", ex);
            }
        }
    }
}
