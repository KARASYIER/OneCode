using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OneCode.Application;
using OneCode.Application.Contracts;
using OneCode.BizImages.Dtos;
using OneCode.Domain.Repositories;
using OneCode.ToolKit.Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OneCode.BizImages
{
    public class BizImageAppService : OneCodeAppService, IBizImageAppService
    {
        private IBizImageRepository _BizImageRepository;
        private IHttpContextAccessor _httpContext;
        private IHostEnvironment _webHostEnvironment;
        private IOptions<OneCodeSettingOptions> _oneCodeSetting;

        public BizImageAppService(
            IOptions<OneCodeSettingOptions> oneCodeSetting,
            IHttpContextAccessor httpContext,
            IWebHostEnvironment webHostEnvironment,
            IBizImageRepository bizImageRepository
            )
        {
            _BizImageRepository = bizImageRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContext;
            _oneCodeSetting = oneCodeSetting;
        }

        public async Task<ResponseReturn> CreateAsync()
        {
            List<BizImageDto> BizImageDtos = new List<BizImageDto>();

            var images = _httpContext.HttpContext.Request.Form.Files;

            if (images != null && images.Count > 0)
            {

                var date = DateTime.Now.ToString("yyyyMMdd");

                var fileFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "upload", date);

                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }

                var url = _oneCodeSetting.Value.UploadUrl + "/" + date + "/";


                var image = images[0];

                var fileExtension = image.FileName.Substring(image.FileName.LastIndexOf('.'));

                if (fileExtension != ".jpg" && fileExtension != ".png")
                {
                    throw new OneCodeBizException("不符合格式的图片");
                }

                var fileName = GuidGenerator.Create().ToString() + fileExtension;

                var filePath = Path.Combine(fileFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);

                    url = url + fileName;

                    //await _BizImageRepository.InsertAsync(new BizImage(id, url, displayOrder));

                    //BizImageDtos.Add(new BizImageDto
                    //{
                    //    SubjectId = id,
                    //    Url = url,
                    //    DisplayOrder = displayOrder
                    //});
                }

                return ResponseReturn.ReturnSuccess(
                    data: url
                    );

            }
            else
            {
                throw new OneCodeBizException("没有获取到图片");
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="salerId"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> CreateQrCode(Guid shopId, Guid salerId)
        {
            var qrtext = $"{_oneCodeSetting.Value.OneCodeQR}?shopId={shopId}&salerId={salerId}";

            var date = DateTime.Now.ToString("yyyyMMdd");

            //文件夹路径
            var fileFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "qrocde", date);

            //创建文件夹
            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }

            //文件名
            var fileName = GuidGenerator.Create().ToString() + ".jpg";

            //文件完整路径
            var filePath = Path.Combine(fileFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var image = QRCodeHelper.GetQRCode(qrtext, 4, System.Drawing.Color.Black, System.Drawing.Color.White, QRCodeGenerator.ECCLevel.M);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            }

            return await ResponseReturn.ReturnSuccessAsync(

                data: _oneCodeSetting.Value.UploadUrl + "/" + date + "/" + fileName
                );

        }

        //public async Task<ListResultDto<BizImageDto>> GetListAsync(Guid id)
        //{
        //    //return SuccessListResut(
        //    //    ObjectMapper.Map<List<BizImage>, List<BizImageDto>>(await _BizImageRepository.GetListAsync(id,))
        //    //    );
        //    return null;
        //}

        //public async Task<ResponseReturn> demo()
        //{
        //    var item1 = new BizImageDto()
        //    {
        //        SubjectId = Guid.NewGuid()
        //    };
        //    var item2 = new BizImageDto()
        //    {
        //        SubjectId = Guid.NewGuid()
        //    };
        //    List<BizImageDto> lst = new List<BizImageDto>();
        //    lst.Add(item1);
        //    lst.Add(item2);
        //    ListResultDto<BizImageDto> lstResult = new ListResultDto<BizImageDto>() { Items = lst };

        //    var retObj = await ResponseReturn.ReturnSuccessAsync(data: lstResult);
        //    return retObj;
        //}


        //public Task<ResponseReturn> error()
        //{
        //    throw new Exception();
        //}
    }
}
