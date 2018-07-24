using App.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace App.Data.Repositories
{
    internal class ProfileRepository : RepositoryBase<Profile>
    {
        public ProfileRepository(IConfiguration config) : base(config)
        {
        }

        public override Profile Create(Profile entity)
        {
            return null;
        }
    }
}