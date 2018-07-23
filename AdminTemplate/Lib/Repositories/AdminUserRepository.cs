using Dapper;
using Lib.Entities;

namespace Lib.Repositories
{
    internal class AdminUserRepository : BaseRepository<AdminUser>
    {
        public AdminUser GetUserByEmail(string email)
        {
            AdminUser result = null;
            string query = @"SELECT * FROM AdminUser WHERE Email = @Email";

            using (var con = CreateConnection())
            {
                con.Open();
                result = con.QueryFirstOrDefault<AdminUser>(query, new { Email = email });
                con.Close();
            }

            return result;
        }
    }
}