using System.ComponentModel.DataAnnotations;

namespace OneCode.Dtos
{
    /// <summary>
    /// 分页请求所需参数
    /// </summary>
    public class PagedInputDto
    {
        [Range(0, int.MaxValue)]
        public int PageSize { get; set; } = 20;

        [Range(0, int.MaxValue)]
        public int PageNo { get; set; } = 1;

    }
}
