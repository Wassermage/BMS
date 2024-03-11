using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string Name { get; set; } = null!;

        // Relationship: AccessControlGroup
        //[ForeignKey(nameof(AccessControlGroupRoom.RoomId))]
        public ICollection<AccessControlGroupRoom> AccessControlGroupRooms { get; } = new List<AccessControlGroupRoom>();
        //public ICollection<AccessControlGroup> AccessControlGroups { get; } = new List<AccessControlGroup>();

    }
}
