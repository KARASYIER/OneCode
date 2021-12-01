using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OneCode.Application.Contracts;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Salers.Dtos;
using OneCode.ToolKit.Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Application
{
    /// <summary>
    /// 分销员业务应用处理
    /// </summary>
    public class SalerAppService : OneCodeAppService, ISalerAppService
    {
        private ISalerRepository _salerRepository;
        private IShopRepository _shopRepository;

        private IOptions<OneCodeSettingOptions> _oneCodeSetting;
        private IHostEnvironment _webHostEnvironment;


        public SalerAppService(
            IOptions<OneCodeSettingOptions> oneCodeSetting,
            IWebHostEnvironment webHostEnvironment,

            ISalerRepository salerRepository,
            IShopRepository shopRepository)
        {
            _oneCodeSetting = oneCodeSetting;
            _webHostEnvironment = webHostEnvironment;

            _salerRepository = salerRepository;
            _shopRepository = shopRepository;
        }

        public async Task<ResponseReturn> CreateAsync(CreateOrUpdateSalerInputDto input)
        {
            var saler = new Saler(GuidGenerator.Create())
            {
                Status = true,
                Type = SalerTypeEnum.Saler
            };

            ObjectMapper.Map(input, saler);

            //
            (saler.ShopId == default(Guid)).CheckBool("必须输入所属的店铺");

            //注册分销员必须选择绑定的店铺
            (!await _shopRepository.AnyAsync(p => p.Id == saler.ShopId && p.IsDeleted == false)).CheckBool("无效的店铺编号");

            //检查手机号是否已经存在
            (await _salerRepository.AnyAsync(p => p.Mobile == saler.Mobile)).CheckBool("该手机号已被注册");

            //生成二维码
            saler.QRCodeUrl = this.GeneratorQRCode(_oneCodeSetting.Value.OneCodeQR + $"?shop={input.ShopId}&salerId={saler.Id}");

            await _salerRepository.InsertAsync(saler, true);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Saler, SalerDto>(saler)
                );
        }

        public async Task<ResponseReturn> UpdateAsync(Guid id, CreateOrUpdateSalerInputBaseDto input)
        {
            var saler = await _salerRepository.GetAsync(id);

            ObjectMapper.Map(input, saler);

            if (await _salerRepository.AnyAsync(p => p.Mobile == saler.Mobile && p.Id != saler.Id))
            {
                throw new OneCodeBizException("该手机号已被注册");
            }

            await _salerRepository.UpdateAsync(saler, true);

            return ResponseReturn.ReturnSuccess(
               data: ObjectMapper.Map<Saler, SalerDto>(saler)
                );
        }

        public async Task<ResponseReturn> UpdateStatusAsync(Guid id)
        {
            var saler = await _salerRepository.GetAsync(id);

            saler.Status = !saler.Status;

            await _salerRepository.UpdateAsync(saler);

            return ResponseReturn.ReturnSuccess();
        }

        public async Task<ResponseReturn> UpdatePasswordAsync(Guid id, UpdateSalerPasswordInputDto input)
        {
            if (input.NewPassword != input.ConfirmPassword)
            {
                throw new OneCodeBizException("两次密码输入不一致错误");
            }

            var saler = await _salerRepository.GetAsync(id);

            if (saler.Password != input.OldPassword)
            {
                throw new OneCodeBizException("原始密码错误");
            }

            saler.Password = input.NewPassword;

            await _salerRepository.UpdateAsync(saler);

            return ResponseReturn.ReturnSuccess();
        }

        public async Task<ResponseReturn> DeleteAsync(Guid id)
        {
            await _salerRepository.DeleteAsync(id);

            return ResponseReturn.ReturnSuccess();
        }

        public async Task<ResponseReturn> LoginAsync(string mobile, string password)
        {
            var saler = await _salerRepository.GetByMobileAsync(mobile);

            if (saler == null)
            {
                throw new OneCodeBizException("用户名或者密码错误");
            }

            if (saler.Password != password)
            {
                throw new OneCodeBizException("用户名或者密码错误");
            }

            return ResponseReturn.ReturnSuccess(
                 data: new
                 {
                     Token = Guid.NewGuid().ToString()
                 }
            );
        }

        public async Task<ResponseReturn> GetAsync(Guid id)
        {
            var saler = await _salerRepository.GetAsync(id);

            return ResponseReturn.ReturnSuccess(
               data: ObjectMapper.Map<Saler, SalerDto>(saler)
                );
        }

        public async Task<ResponseReturn> GetListAsync(GetSalersInputDto input)
        {
            var total = await _salerRepository.GetCountAsync(
                                input.Name,
                                input.Mobile,
                                input.ShopName,
                                input.ShopId,
                                input.Type,
                                input.Status);

            var salers = await _salerRepository.GetListAsync(
                                input.Name,
                                input.Mobile,
                                input.ShopName,
                                input.ShopId,
                                input.Type,
                                input.Status,
                                input.PageNo,
                                input.PageSize
                                );

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<GetSalersResultDto>
                {
                    PageNo = input.PageNo,
                    PageSize = input.PageSize,
                    TotalCount = total,
                    Items = ObjectMapper.Map<List<Saler>, List<GetSalersResultDto>>(salers)
                });

        }

        public async Task<ResponseReturn> GetTypeAsync()
        {
            return await ResponseReturn.ReturnSuccessAsync(
                data: ReturnOptionListResult(typeof(SalerTypeEnum))
                );
        }

        private string GeneratorQRCode(string text, int pixels = 4)
        {
            //var qrtext = _oneCodeSetting.Value.SalerQRCodeFormat.Replace("{shopId}", shopId.ToString()).Replace("{salerId}", salerId.ToString());

            var date = DateTime.Now.ToString("yyyyMMdd");

            //文件夹路径
            var fileFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "upload", date);

            //创建文件夹
            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }

            //文件名
            var fileName = GuidGenerator.Create().ToString() + ".jpg";

            //文件完整路径
            var filePath = Path.Combine(fileFolder, fileName);

            //保存图片
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //生成二维码流
                var image = QRCodeHelper.GetQRCode(text, pixels, System.Drawing.Color.Black, System.Drawing.Color.White, QRCodeGenerator.ECCLevel.M);

                //保存
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            return _oneCodeSetting.Value.UploadUrl + "/" + date + "/" + fileName;
        }
    }
}
