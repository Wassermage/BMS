using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    public class TemperatureReader
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string Name { get; set; } = "Temperature Reader";

        // Relationship: Room
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public ICollection<TemperatureReadout> Readouts { get; set; } = new List<TemperatureReadout>();
    }
}
