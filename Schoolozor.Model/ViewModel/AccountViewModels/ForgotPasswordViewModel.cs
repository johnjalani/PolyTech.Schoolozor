using System.ComponentModel.DataAnnotations;

namespace Schoolozor.Model.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
