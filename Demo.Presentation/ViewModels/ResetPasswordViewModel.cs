using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class ResetPasswordViewModel
    {

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password Is Required")]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Confirm Password Is Required")]

        public string ConfirmPassword { get; set; } = null!;
    }
}
