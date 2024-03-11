using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    // Access Control Group Model
    public class AccessControlGroup
    {
        [Key]
        public int Id { get; set; }

        // TODO: Enforce unique values
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "This field is required.")]
        public TimeSpan AllowedEntryHour { get; set; }

        // Relationship: Employee
        public ICollection<Employee> Employees { get; } = new List<Employee>();

        // Relationship: Room
        //[ForeignKey(nameof(AccessControlGroupRoom.AccessControlGroupId))]
        public ICollection<AccessControlGroupRoom> AccessControlGroupRooms { get; } = new List<AccessControlGroupRoom>();
        //public ICollection<Room> Rooms { get; } = new List<Room>();
    }
}
