﻿namespace Common
{
    public class SiteSettings
    {
        public CommitionSettings CommitionSettings { get; set; }
        public HangFireSettings HangFireSettings { get; set; }
        public string ElmahPath { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public string PassKeyEncrypt { get; set; }
        public string UriToken { get; set; }
        public RabbitOptions RabbitMQSettings { get; set; }
        
    }

    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumic { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
    public class HangFireSettings
    {
        public string Duration { get; set; }
        public string HangFirePath { get; set; }
    }
    public class CommitionSettings
    {
        public int DurationMin { get; set; }
    }
    public class RabbitOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }

        public int Port { get; set; } = 5672;

        public string VHost { get; set; } = "/";
    }

}
