using App.Domain.Entities;

namespace App.Domain.Repositories
{
    public interface IAdminUserRepository : IBaseRepository<AdminUser>
    {
        AdminUser GetByEmail(string email);
    }
}