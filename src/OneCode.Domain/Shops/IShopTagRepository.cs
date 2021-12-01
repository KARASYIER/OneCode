using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IShopTagRepository : IRepository<ShopTag>
    {
        Task<List<ShopTag>> GetListByShopId(Guid shopId);

        Task<List<ShopTag>> GetListByTagId(Guid tagId);
    }

}
