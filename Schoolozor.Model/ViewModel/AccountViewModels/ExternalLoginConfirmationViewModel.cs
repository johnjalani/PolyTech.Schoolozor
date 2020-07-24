using System.ComponentModel.DataAnnotations;

namespace Schoolozor.Model.ViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
