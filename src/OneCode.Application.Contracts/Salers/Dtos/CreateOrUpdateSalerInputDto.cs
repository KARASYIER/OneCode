namespace OneCode.Salers.Dtos
{
    public class CreateOrUpdateSalerInputDto :CreateOrUpdateSalerInputBaseDto
    {
        /// <summary>
        /// 账户密码
        /// </summary>
        public virtual string Password { get; set; }
    }
}
