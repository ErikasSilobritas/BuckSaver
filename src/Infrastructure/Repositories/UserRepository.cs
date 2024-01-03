using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            return _connection.Query<UserEntity>("SELECT * FROM users WHERE \"isDeleted\" = false");
        }

        public async Task<UserEntity?> GetUser(int id)
        {
            string sql = "SELECT * FROM users WHERE id=@UserId AND \"isDeleted\" = false";
            var queryArguments = new
            {
                UserId = id
            };
            return _connection.QuerySingleOrDefault<UserEntity>(sql, queryArguments);
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            string sql = "INSERT INTO users (name, address) VALUES (@UserName, @UserAddress) RETURNING *";
            var queryArguments = new
            {
                UserName = user.Name,
                UserAddress = user.Address
            };
            return _connection.QuerySingle<UserEntity>(sql, queryArguments);
        }
    }
}
