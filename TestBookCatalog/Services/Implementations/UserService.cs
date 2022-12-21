using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetAsync(string phoneNumber)
        {
            var user = await _userRepository.GetAsync(phoneNumber);

            if (user == null)
                throw new ArgumentException("user not found");

            return user;
        }

        public async Task<User> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
                throw new ArgumentException("user not found");

            return user;
        }

        public async Task<string> SaveAsync(Login model)
        {
            if (!await _userRepository.IsExistAsync(model.PhoneNumber))
            {
                await _userRepository.SaveAsync(new User
                {
                    PhoneNumber = model.PhoneNumber,
                    RefreshToken = Guid.NewGuid(),
                    RoleId = (await _userRepository.GetRoleAsync("user")).Id
                });
            }

            return $"SMS from TestBookCatalog. \nCode: {FastFields.SMSCode}";
        }
    }
}
