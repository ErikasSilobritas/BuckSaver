using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<UserEntity?> GetUser(int id);
        Task<UserEntity> CreateUser(UserEntity user);

    }
}
