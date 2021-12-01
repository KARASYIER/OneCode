using OneCode.Domain;
using System;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IAdminUserRepository : IRepository<AdminUser, Guid>
    {
    }
}
