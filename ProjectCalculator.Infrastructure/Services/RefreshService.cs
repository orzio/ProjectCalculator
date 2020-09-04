using AutoMapper;
using ProjectCalculator.Api.Repositories;
using ProjectCalculator.Core.Domain;
using ProjectCalculator.Core.Repositories;
using ProjectCalculator.Infrastructure.Data;
using ProjectCalculator.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Services
{
    public class RefreshService : IRefreshService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RefreshService(ITokenRepository tokenRepository, IUserRepository userRepository, IMapper mapper)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task UpdateTokenAsync(Guid userId, string jwtToken, string refreshToken)
        {
            var token = await _tokenRepository.GetAsync(userId);
            if (token != null)
            {
                if(token.RefreshToken != refreshToken)
                {
                    throw new Exception("Invalid refresh token!");
                }

                token.JwtToken = jwtToken;
                token.RefreshToken = refreshToken;
                await _tokenRepository.UpdateAsync(token);
            }
            else
            {
                var newToken = new Token()
                {
                    TokenId = Guid.NewGuid(),
                    JwtToken = jwtToken,
                    RefreshToken = refreshToken,
                    UserId = userId,
                    
                };
                await _tokenRepository.AddAsync(newToken);
            }
        }

        public async Task<TokenDto> GetToken(Guid userId)
        {
            var token =  await _tokenRepository.GetAsync(userId);
            return _mapper.Map<Token, TokenDto>(token);
        }

        public async Task DeleteTokens(Guid userId)
        {
            await _tokenRepository.RemoveAsync(userId);
        }

    }
}
