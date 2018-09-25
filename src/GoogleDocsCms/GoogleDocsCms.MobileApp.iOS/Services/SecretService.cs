using GoogleDocsCms.MobileApp.Interfaces;

namespace GoogleDocsCms.MobileApp.iOS.Services
{
    public class SecretService : ISecretService
    {
        public string GetSecretFilePath() => "secret.json";
    }
}
