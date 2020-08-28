using ProjectCalculator.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Services
{
    public interface IUserService:IService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(Guid userId, string email,
            string firstname, string lastname, string password, string role);
        Task LoginAsync(string email, string password);
    }
}
