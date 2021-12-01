using Microsoft.AspNetCore.Mvc;
using OneCode.ToolKit.Http;
using OneCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Uow;

namespace OneCode.Controllers
{
    [Route("api/onecode/[controller]/[action]/{id}")]
    public class ProductController : AbpController
    {

        private const string GET_PRODUCT_URL = "http://app.zizailvyou.com/zzlywechat/Member/GetProductionForPromot";


        [HttpGet]
        public Task<ResponseReturn> GetWxProductsAsync(int id = 1)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("productTypeid", id.ToString());

            var data = JsonSerializer.Deserialize<ResultViewModel<ProductBusViewModel>>(HttpTools.GetHttpWebResponseReturnString(
                GET_PRODUCT_URL,
                null,
                $"{{\"productTypeid\": {id}}}",
                "application/json"
                 )); ;

            return ResponseReturn.ReturnSuccessAsync(
                data: data.ResultData);
        }
    }
}
