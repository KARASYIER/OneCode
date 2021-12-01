using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface ITagRepository : IRepository<Tag, Guid>
    {
        /// <summary>
        /// 根据名称获取Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        Task<Tag> GetByNameAsync(string tagName);

        /// <summary>
        /// 条件查询数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter);

        /// <summary>
        /// 条件查询集合
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Tag>> GetListAsync(string filter, int pageNo = 1, int pageSize = 20);


    }
}
