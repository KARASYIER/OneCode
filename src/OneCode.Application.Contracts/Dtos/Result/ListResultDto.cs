using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OneCode.Dtos
{
    /// <summary>
    /// 表示一个有结果集(不含分页)的响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ListResultDto<T> : ResultDto, IListResult<T>
    {
        private IReadOnlyList<T> _items;

        public IReadOnlyList<T> Items
        {
            get { return _items ?? (new List<T>()); }
            set { _items = value; }
        }
    }
}
