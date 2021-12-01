using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OneCode.Dtos
{
    /// <summary>
    /// 表示带有分页结果集的响应
    /// </summary>
    public class PagedListResultDto<T> : ResultDto, IPagedResult<T>
    {
        private IReadOnlyList<T> _items;

        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }


        public long TotalCount { get; set; }

        public int PageSize { get; set; } = 20;

        public int PageNo { get; set; } = 1;
    }
}
