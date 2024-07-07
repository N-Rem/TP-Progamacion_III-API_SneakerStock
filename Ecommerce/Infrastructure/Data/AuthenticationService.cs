using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.User;

namespace Infrastructure.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IRepositoryUser repositoryUser, IOptions<AuthenticationServiceOptions> options)
        {
            _repositoryUser = repositoryUser;
            _options = options.Value;
        }

        public User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _repositoryUser.GetByEmail(authenticationRequest.Email);

            if (user == null) return null;

            if (user.Type == User.UserType.Client
              || user.Type == User.UserType.Admin)
            {
                if (user.Password == authenticationRequest.Password) return user;
            }

            return null;
        }

        public string Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new Exception("no encontrado");
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Name));
            claimsForToken.Add(new Claim("role", user.Type.ToString() ?? UserType.Admin.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
              _options.Issuer,
              _options.Audience,
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
