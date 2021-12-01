using OneCode.Domain;
using OneCode.Domain.Repositories;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.EntityFrameworkCore.Repositories
{
    public class OrderDetailRepository : EfCoreRepository<OneCodeDbContext, OrderDetail, Guid>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
