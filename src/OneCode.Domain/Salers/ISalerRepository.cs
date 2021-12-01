using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface ISalerRepository : IRepository<Saler, Guid>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<Saler>> GetListAsync(Guid shopId);

        /// <summary>
        /// 根据手机号查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<Saler> GetByMobileAsync(string filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="shopId"></param>
        /// <param name="salerType"></param>
        /// <param name="salerStatus"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string name, string mobile, string shopName, Guid? shopId, SalerTypeEnum? salerType, bool? salerStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="shopId"></param>
        /// <param name="salerType"></param>
        /// <param name="salerStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Saler>> GetListAsync(string name, string mobile, string shopName, Guid? shopId, SalerTypeEnum? salerType, bool? salerStatus, int pageNo = 1, int pageSize = 20);

    }
}
