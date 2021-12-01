using Microsoft.AspNetCore.Mvc;
using OneCode.Application;
using OneCode.Application.Contracts;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.Draws.Dtos;
using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.ToolKit.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCode.Finances
{
    public class DrawAppService : OneCodeAppService, IDrawAppService
    {
        private IShopRepository _shopRepository;
        private ISalerRepository _salerRepository;
        private IDrawRepository _drawRepository;

        public DrawAppService(
            IShopRepository shopRepository,
            ISalerRepository salerRepository,
            IDrawRepository drawRepository
            )
        {
            _shopRepository = shopRepository;
            _salerRepository = salerRepository;
            _drawRepository = drawRepository;
        }

        /// <summary>
        /// 新增提现
        /// 优化:采用CurrentUser对用户权限进行控制,
        /// input中的ShopId,SalerId,Name,Mobile弃用,Amount只进行验证
        /// </summary>
        public async Task<ResponseReturn> CreateAsync(CreateDrawInputDto input)
        {
            var shop = (await _shopRepository.FindAsync(CurrentUser.ShopId)).CheckNullOrDeleted();

            (!shop.OwnerId.HasValue).CheckBool("提现需要先绑定店铺负责人,由负责人申请提现");

            (shop.OwnerId != CurrentUser.Id).CheckBool("当前申请人非该店铺负责人,无法申请提现");

            (shop.CommisionAvailable != input.Amount).CheckBool("当前可申请提现的金额与申请的金额不匹配");

            (shop.CommisionApplying > 0).CheckBool("当前有正在申请的提现,无法进行多次申请");

            var draw = new Draw(GuidGenerator.Create())
            {
                ShopId = CurrentUser.ShopId,
                ShopName = shop.Name,

                DrawStatus = DrawStatusEnum.Approving,

                Amount = input.Amount,

                Name = CurrentUser.Name,
                Mobile = CurrentUser.Mobile
            };

            shop.CommisionApplying += input.Amount;
            shop.CommisionAvailable -= input.Amount;

            draw.RemainCommision = shop.CommisionAvailable;

            await _drawRepository.CreateAsync(draw, shop);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 审核提现
        /// </summary>
        [HttpPut]
        public async Task<ResponseReturn> ApproveAsync(Guid id)
        {
            var draw = (await _drawRepository.FindAsync(id)).CheckNullOrDeleted();

            (draw.DrawStatus == DrawStatusEnum.Approved).CheckBool("该笔提现已审核");

            var shop = (await _shopRepository.FindAsync(draw.ShopId)).CheckNullOrDeleted();

            (shop.CommisionApplying != draw.Amount).CheckBool("当前可提现的金额与提交的不符合,请联系相关技术人员");

            //扣除店铺申请中的佣金额度 
            shop.CommisionApplying -= draw.Amount;

            //更改审核状态
            draw.DrawStatus = DrawStatusEnum.Approved;
            draw.ConfirmTime = DateTime.Now;

            //记录当前操作人的Id
            draw.LastModifierId = CurrentUser.Id;

            (!shop.OwnerId.HasValue).CheckBool("店铺尚未绑定负责人,无法创建提现记录");

            var record = new CommisionRecord(GuidGenerator.Create())
            {
                CommisionAmount = draw.Amount.ToNegative(),
                CommisionAvailable = shop.CommisionAvailable,

                RecordFlag = RecordFlag.Draw,
                RelationId = draw.Id,
                ShopId = draw.ShopId,
                ShopName = draw.ShopName,

                SalerId = draw.OwnerId,
                SalerName = draw.Name,
            };

            await _drawRepository.Approved(draw, shop, record);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询单笔提现记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetAsync(Guid id)
        {
            var draw = (await _drawRepository.FindAsync(id)).CheckNullOrDeleted();

            return ResponseReturn.ReturnSuccess(data: ObjectMapper.Map<Draw, DrawDto>(draw));
        }

        /// <summary>
        /// 条件查询集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetListAsync(GetDrawsInputDto input)
        {
            var total = await _drawRepository.GetCountAsync(input.Filter,
                                                            input.ShopId,
                                                            input.Status,
                                                            input.StartDate,
                                                            input.EndDate);

            var draws = await _drawRepository.GetListAsync(input.Filter,
                                                           input.ShopId,
                                                           input.Status,
                                                           input.StartDate,
                                                           input.EndDate,
                                                           input.PageNo,
                                                           input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<DrawDto>
                {
                    Items = ObjectMapper.Map<List<Draw>, List<DrawDto>>(draws),
                    TotalCount = total,
                    PageNo = input.PageNo,
                    PageSize = input.PageSize
                });

        }

        /// <summary>
        /// 获取提现状态选项
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseReturn> GetStatusOptionsAsync()
        {
            return await ResponseReturn.ReturnSuccessAsync(
                data: ReturnOptionListResult(typeof(DrawStatusEnum))
                );
        }
    }
}
