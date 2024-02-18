using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IConfiguration _configuration { get; }

        public LoginService(
            IUserRepository userRepository,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto login)
        {
            var baseUrl = new UserEntity();

            if (login != null && !string.IsNullOrWhiteSpace(login.Email))
            {
                baseUrl = await _userRepository.FindByLogin(login.Email);

                if (baseUrl == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao Autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUrl.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  //Jti é o Id to Token
                            new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationdate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = Createtoken(identity, createDate, expirationdate, handler);

                    return SuccessObject(createDate, expirationdate, token, login);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao Autenticar"
                };
            }
        }

        private string Createtoken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto login)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = login.Email,
                message = "Usuário logado com sucesso."
            };
        }
    }
}
