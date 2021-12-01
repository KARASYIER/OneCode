namespace OneCode
{
    public class OneCodeSettingOptions
    {
        public const string OneCodeSetting = "OneCodeSetting";

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 一店一码二维码链接
        /// </summary>
        public string OneCodeQR { get; set; }

        /// <summary>
        /// 上传图片的url
        /// </summary>
        public string UploadUrl { get; set; }


        public string ProductUrlFormat_Bus { get; set; }


        public string ProductUrlFormat_Hotel { get; set; }


        public string ProductUrlFormat_Vacation { get; set; }

    }
}
