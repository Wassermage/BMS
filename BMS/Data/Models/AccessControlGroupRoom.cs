using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    public class AccessControlGroupRoom
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(AccessControlGroup))]
        public int AccessControlGroupId { get; set; }
        public AccessControlGroup AccessControlGroup { get; set; } = null!;

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}
