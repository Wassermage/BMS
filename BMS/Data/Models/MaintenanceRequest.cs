using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Data.Models
{
    public enum RequestStatus
    {
        New,
        WorkStarted,
        OnHold,
        Closed
    }

    public class MaintenanceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public RequestStatus Status { get; set; } = RequestStatus.New;

        public string StatusFormatted => Status switch
        {
            RequestStatus.New => "New",
            RequestStatus.WorkStarted => "Work started",
            RequestStatus.OnHold => "On hold",
            RequestStatus.Closed => "Closed",
            _ => "Unknown"
        };

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length of that field is 100.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(4000, ErrorMessage = "Maximum length of that field is 4000.")]
        public string Description {  get; set; } = null!;

        [Required(ErrorMessage = "This field is required.")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedDateFormatted => CreatedDate.ToString("dd.MM.yyyy HH:mm");

        [Required(ErrorMessage = "This field is required.")]
        [ForeignKey(nameof(Employee))]
        public int? CreatedById { get; set; }
        public Employee CreatedBy { get; set; } = null!;

        [ForeignKey(nameof(Employee))]
        public int? AssignedToId { get; set; }
        public Employee? AssignedTo { get; set; }
    }
}
