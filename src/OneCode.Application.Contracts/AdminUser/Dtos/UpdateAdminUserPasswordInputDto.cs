namespace OneCode.AdminUser.Dtos
{
    public class UpdateAdminUserPasswordInputDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
