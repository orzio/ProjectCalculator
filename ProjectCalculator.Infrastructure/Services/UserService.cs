using AutoMapper;
using ProjectCalculator.Api.Repositories;
using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Services
{
    //Service mapuje usera pobranego z repozytorium na dtosa. Dlatego go potrzebujemy .
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        private readonly IRefreshService _refreshService;
        public UserService(IUserRepository userRepository, IMapper mapper, IEncrypter encrypter, IRefreshService refreshService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
            _refreshService = refreshService;
        }
        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }


        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }
            var hash = _encrypter.CreatePasswordHash(password, user.Salt);
            var isEqual = true;

            for(int i =0; i< user.Password.Length; i++)
            {
                if (user.Password[i] != hash[i])
                    isEqual = false;
            }

            if (isEqual)
            {
                await _refreshService.DeleteTokens(user.Id);
                return;
            }
            throw new Exception("Invalid credentials");
        }

        public async Task RegisterAsync(Guid userId, string email, string firstname, string lastname, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }

            _encrypter.CreatePasswordHash(password);

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetPassworHash();

            user = new User(userId, email, firstname, lastname, role, hash, salt);
            await _userRepository.AddAsync(user);
        }
    }
}
