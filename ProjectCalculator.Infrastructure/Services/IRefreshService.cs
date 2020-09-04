using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Services
{
    public interface IRefreshService:IService
    {
        string GenerateRefreshToken();
        Task UpdateTokenAsync(Guid userId, string jwtToken, string refreshToken);
        Task<TokenDto> GetToken(Guid userId);
        Task DeleteTokens(Guid userId);
    }
}
