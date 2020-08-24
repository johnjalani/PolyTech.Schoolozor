using System;
using System.ComponentModel.DataAnnotations;
using static Schoolozor.Shared.Extensions.CustomDataAnnotations;

namespace Schoolozor.Model.ViewModel.SchoolViewModels
{
    public class SchoolYearViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CheckDateNotFlip(nameof(End))]
        [Display(Name = "Start date")]
        public DateTime? Start { get; set; }
        [Required]
        [NotPastDate]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? End { get; set; }
        public Guid? SchoolId { get; set; }

        public string StartDesc
        {
            get
            {
                if (Start.HasValue)
                {
                    return Start.Value.ToShortDateString();
                }
                return string.Empty;
            }
        }
        public string EndDesc
        {
            get
            {
                if (End.HasValue)
                {
                    return End.Value.ToShortDateString();
                }
                return string.Empty;
            }
        }
    }
}
