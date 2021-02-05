using Common;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Entities.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSettings _siteSetting;
        private readonly SignInManager<User> signInManager;

        public JwtService(IOptionsSnapshot<SiteSettings> settings, SignInManager<User> signInManager)
        {
            _siteSetting = settings.Value;
            this.signInManager = signInManager;
        }

        public async Task<AccessToken> GenerateAsync(User user)
        {
            try
            {
                var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

                var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey); //must be 16 character
                var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
                var claims = await _getClaimsAsync(user);

                var descriptor = new SecurityTokenDescriptor
                {
                    Issuer = _siteSetting.JwtSettings.Issuer,
                    Audience = _siteSetting.JwtSettings.Audience,
                    IssuedAt = DateTime.Now,
                    NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                    Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                    SigningCredentials = signingCredentials,
                    EncryptingCredentials = encryptingCredentials,
                    Subject = new ClaimsIdentity(claims)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

                return new AccessToken(securityToken);
            }
            catch(Exception ex)
            {
                using (FileStream fs = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "errorStart.txt")))
                {
                    Byte[] title = new UTF8Encoding(true).GetBytes(ex.Message);
                    fs.Write(title, 0, title.Length);
                }
                throw;

            }
           
        }

        public async Task<IEnumerable<Claim>> ReadClaimsAsync(string token)
        {
            try
            {
                var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

                var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey); //must be 16 character
                var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

                var tokenHandler = new JwtSecurityTokenHandler();

                var securityToken = tokenHandler.ReadJwtToken(token);

                IEnumerable<Claim> claims = securityToken.Claims;

                return claims;
            }
            catch (Exception ex)
            {
                using (FileStream fs = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "errorStart.txt")))
                {
                    Byte[] title = new UTF8Encoding(true).GetBytes(ex.Message);
                    fs.Write(title, 0, title.Length);
                }
            }

            return null;
        }

        private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
        {
            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            return list;
        }
    }
}
