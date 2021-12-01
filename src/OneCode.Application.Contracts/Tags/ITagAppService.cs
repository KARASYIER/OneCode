using OneCode.Dtos;
using OneCode.Tags.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    /// <summary>
    /// 店铺的标签操作管理接口(管理员操作)
    /// </summary>
    public interface ITagAppService
    {
        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> CreateAsync(CreateOrUpdateTagInputDto input);

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateAsync(Guid id, CreateOrUpdateTagInputDto input);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> DeleteAsync(Guid id);

        /// <summary>
        /// 获取标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetAsync(Guid id);

        /// <summary>
        /// 获取标签集合(不分页)
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetListAsync(GetListInputDto input);
    }
}
