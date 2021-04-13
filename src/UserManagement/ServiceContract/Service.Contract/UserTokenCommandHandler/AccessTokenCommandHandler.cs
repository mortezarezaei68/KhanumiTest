using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceContract.Command.UserTokenCommands;
using UserManagement.Domains;

namespace Service.Contract.UserTokenCommandHandler
{
    public class AccessTokenCommandHandler:ITransactionalCommandHandlerMediatR<AccessTokenCommandRequest,AccessTokenCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEventBus _eventBus;

            public AccessTokenCommandHandler(UserManager<User> userManager, IConfiguration configuration, IEventBus eventBus)
        {
            _userManager = userManager;
            _configuration = configuration;
            _eventBus = eventBus;
        }

        public async Task<AccessTokenCommandResponse> Handle(AccessTokenCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AccessTokenCommandResponse> next)
        {
            
            
            var accessToken = await CreateAccessTokenAsync(request.Roles,request.SubjectId,request.UserType);
            var refreshToken = new RefreshTokenCommandRequest
            {
                SubjectId = request.SubjectId,
                UserId = request.UserId,
                UserType = request.UserType
            };
            var refreshTokenValue= await _eventBus.Issue(refreshToken);
            
            var tokenViewModel = new TokenViewModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue.Data.RefreshToken
            };
            return new AccessTokenCommandResponse(true,ResultCode.Success,tokenViewModel);
        }
        private async Task<string>  CreateAccessTokenAsync(List<string> roles,string subjectId,UserType userType)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, subjectId),
                new Claim(JwtClaimTypes.Audience,_configuration["JwtToken:Audience"])
            });
            if (userType==UserType.admin)
            {
                var roleClaims = roles.Select(a => new Claim(JwtClaimTypes.Role, a));
                claimsIdentity.AddClaims(roleClaims);
            }


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var encryptionkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:EncryptedKey"]));
            var encryptingCredentials = new EncryptingCredentials(encryptionkey, SecurityAlgorithms.Aes128KW,
                SecurityAlgorithms.Aes128CbcHmacSha256);
            var nowDateTime = DateTime.UtcNow;
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtToken:Issuer"],
                IssuedAt = nowDateTime,
                NotBefore = nowDateTime,
                Expires = nowDateTime.AddSeconds(int.Parse(_configuration["JwtToken:AccessTokenExpiredTime"])),
                SigningCredentials = signingCredentials,
                Subject = claimsIdentity
                // EncryptingCredentials = encryptingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

    }
}