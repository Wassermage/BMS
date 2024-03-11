using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        // TODO: Create separate property for Employee Number (eg. "E95022938")

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string LastName { get; set; } = null!;

        public string FullName => string.Join(" ", FirstName, LastName);

        [Required(AllowEmptyStrings = true, ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string? JobTitle { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; } = DateTime.Today;

        // Relationship: AccessControlGroup
        [ForeignKey(nameof(AccessControlGroup))]
        public int AccessControlGroupId { get; set; } = 1;
        public AccessControlGroup AccessControlGroup { get; set; } = null!;
    }
}
