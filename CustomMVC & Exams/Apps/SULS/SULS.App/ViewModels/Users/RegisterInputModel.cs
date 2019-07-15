using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Users
{
    public class RegisterInputModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "")]
        public string Username { get; set; }

        [RequiredSis]
        public string Email { get; set; }

        [RequiredSis]
        [StringLengthSis(6, 20, "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }

        [RequiredSis]
        [StringLengthSis(6, 20, "Password must be between 6 and 20 characters.")]
        public string ConfirmPassword { get; set; }
    }
}
