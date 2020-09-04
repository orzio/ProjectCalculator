using Microsoft.EntityFrameworkCore;
using ProjectCalculator.Core.Domain;
using ProjectCalculator.Core.Repositories;
using ProjectCalculator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _context;

        public TokenRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Token token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<Token> GetAsync(Guid userId)
           => await _context.Tokens.SingleOrDefaultAsync(x => x.UserId == userId);


        public async Task RemoveAsync(Guid userId)
        {
            var token = await GetAsync(userId);
            if (token != null)
            {
                _context.Tokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Token token)
        {
            _context.Tokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
