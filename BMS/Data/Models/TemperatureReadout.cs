using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    public class TemperatureReadout
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Temperature value is required.")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required(ErrorMessage = "Temperature readout time is required.")]
        public DateTime ReadoutTime { get; set; } = DateTime.Now;
        
        // Relationship: TemperatureReader
        [ForeignKey(nameof(TemperatureReader))]
        public int TemperatureReaderId { get; set; }
        public TemperatureReader TemperatureReader { get; set; } = null!;

    }
}
