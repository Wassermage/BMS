using System.ComponentModel.DataAnnotations;

namespace BMS.Data.Models
{
    public class TemperatureReader
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Location (Room) of the reader is required.")]
        public Room Room { get; set; }
    }
}
