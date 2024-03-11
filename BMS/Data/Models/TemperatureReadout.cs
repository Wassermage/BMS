using System.ComponentModel.DataAnnotations;

namespace BMS.Data.Models
{
    public class TemperatureReadout
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Temperature reader is required.")]
        public TemperatureReader TemperatureReader { get; set; } = null!;

        [Required(ErrorMessage = "Temperature value is required.")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required(ErrorMessage = "Temperature readout time is required.")]
        public DateTime readoutTime { get; set; }
    }
}
