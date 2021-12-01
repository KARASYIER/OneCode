using OneCode.Dtos;
using OneCode.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Services;

namespace OneCode.Application
{
    /* Inherit your application services from this class.
     */
    public abstract class OneCodeAppService : ApplicationService
    {
        private User _currentUser;

        protected OneCodeAppService()
        {
            LocalizationResource = typeof(OneCodeResource);

            _currentUser = new User
            {
                Id = Guid.Parse("57A50C57-CB0D-35E9-2B0F-39FA07AA972B"),
                ShopId = Guid.Parse("022C66BD-88F3-D3AC-8389-39FA07730408"),
                Name = "Caoyi",
                Mobile = "13167006350"
            };
        }

        public new User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    throw new OneCodeBizException("获取用户信息失败,请重新登陆");
                }

                return _currentUser;
            }
        }

        /// <summary>
        /// 返回枚举的类型的Name/value选项集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ListResultDto<OptionResultDto> ReturnOptionListResult(Type type)
        {
            List<OptionResultDto> list = new List<OptionResultDto>();

            foreach (var e in Enum.GetValues(type))
            {
                // 转换成Description后添加至List
                System.ComponentModel.DescriptionAttribute attr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true).SingleOrDefault() as System.ComponentModel.DescriptionAttribute;

                if (attr != null)
                {
                    list.Add(new OptionResultDto
                    {
                        Name = attr.Description,
                        Value = ((int)e).ToString()

                    });
                }
            }

            return new ListResultDto<OptionResultDto>
            {
                Items = list
            };
        }

    }
}
