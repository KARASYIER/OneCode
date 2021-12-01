using OneCode.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OneCode.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class OneCodeController : AbpController
    {
        protected OneCodeController()
        {
            LocalizationResource = typeof(OneCodeResource);
        }
    }
}