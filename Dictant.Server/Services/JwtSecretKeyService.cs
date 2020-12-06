
namespace Dictant.Server.Services
{
    public class JwtSecretKeyService
    {
        public readonly string SecretKey;
        public JwtSecretKeyService(string secretKey)
        {
            this.SecretKey = secretKey;
        }
    }
}
