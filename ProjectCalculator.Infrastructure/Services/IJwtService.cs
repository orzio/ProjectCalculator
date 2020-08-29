using ProjectCalculator.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Services
{
    public interface IJwtService
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
