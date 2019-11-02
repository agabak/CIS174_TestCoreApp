using System.ComponentModel.DataAnnotations;

namespace CIS174_TestCoreApp.Models
{
    public class AccomplishmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Date of Accomplishment")]
        public string DateOfAccomplishment { get; set; }
        public int PersonId { get; set; }

    }
}
