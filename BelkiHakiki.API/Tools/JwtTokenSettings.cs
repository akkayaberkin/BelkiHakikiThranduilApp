namespace BelkiHakiki.API.Tools
{
    public class JwtTokenSettings
    {/*
      * ValidAudience = "http://localhost",
        ValidIssuer = "http://localhost",
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true, //token ın süresini doğrulasın.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdasdasdasdsweq123")),
        ValidateIssuerSigningKey = true  
      */
        public const string Issuer = "http://localhost";
        public const string Audience = "http://localhost";
        public const string Key = "berkinberasdkinberkasdinberkin.";
        public const int Expire = 30;
    }
}
