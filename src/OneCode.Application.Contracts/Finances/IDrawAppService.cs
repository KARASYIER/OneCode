using OneCode.Draws.Dtos;
using OneCode.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    /// <summary>
    /// 提现
    /// </summary>
    public interface IDrawAppService
    {
        /// <summary>
        /// 创建一笔提现记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> CreateAsync(CreateDrawInputDto input);

        /// <summary>
        /// 查询单笔提现记录
        /// </summary>
        /// <param name="drawId"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetAsync(Guid id);

        /// <summary>
        /// [店铺负责人]分页查询提现记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetListAsync(GetDrawsInputDto input);

        /// <summary>
        /// 审批通过一笔提现
        /// </summary>
        /// <param name="drawId"></param>
        /// <returns></returns>
        Task<ResponseReturn> ApproveAsync(Guid drawId);

        /// <summary>
        /// 获取提现状态选项集合
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetStatusOptionsAsync();

    }
}
