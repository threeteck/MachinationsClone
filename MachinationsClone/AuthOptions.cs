using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MachinationsClone
{
    public class AuthOptions
    {
        public const string ISSUER = "Issuer";
        public const string AUDIENCE = "ReactClient";
        const string KEY = "machinations_secret_key";
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}