using Domain.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetUser>> GetUsers()
        {
            var allUsers = await _userRepository.GetUsers();

            return allUsers.Select(i => new GetUser
            {
                Name = i.Name,
                Address = i.Address
            }).ToList();
        }

        public async Task<GetUser> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user is null)
            {
                throw new UserNotFoundException();
            }

            var userRequest = new GetUser
            {
                Name = user.Name,
                Address = user.Address
            };
            return userRequest;
        }

        public async Task<CreateUser> CreateUser(CreateUser createUser)
        {
            UserEntity user = new UserEntity
            {
                Name = createUser.Name,
                Address = createUser.Address
            };

            var requestedUser = await _userRepository.CreateUser(user);

            var createdUser = new CreateUser
            {
                Name = requestedUser.Name,
                Address = requestedUser.Address
            };
            return createdUser;

        }
    }
}
