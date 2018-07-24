using App.Domain.Entities;
using App.Domain.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace App.Data.Repositories
{
    internal class AdminUserRepository : RepositoryBase<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(IConfiguration config) : base(config)
        {
        }

        public AdminUser GetByEmail(string email)
        {
            string query = @"SELECT * FROM AdminUser(NOLOCK) WHERE Email = @Email";
            AdminUser result = null;

            using (connection)
            {
                result = connection.QueryFirstOrDefault<AdminUser>(query, new { Email = email });
                connection.Close();
            }
            return result;
        }
    }
}