﻿using Renova.Domain.Model.Dto;
using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Renova.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Renova.Service.Queries.Auth;

namespace Renova.Service.Handlers.Auth
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery,LoginDto>
    {
        private readonly RenovaDbContext _context;
        private readonly SettingsWebApi _settings;

        public LoginQueryHandler(RenovaDbContext context, SettingsWebApi settings)
        {
            _context = context;
            _settings = settings;
        }

        public async Task<LoginDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u=>u.Email == request.Email,cancellationToken);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,request.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AuthSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _settings.AuthSettings.Issuer,
            audience: _settings.AuthSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
