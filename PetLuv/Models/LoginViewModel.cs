using System.ComponentModel.DataAnnotations;

namespace PetLuv.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Quên nhập Email kìa bae!")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu đâu rồi?")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}