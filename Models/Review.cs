using System.ComponentModel.DataAnnotations;

namespace LocalServiceBooking.Models;

public class Review
{
    public int Id { get; set; }

    [Required] public int BookingId { get; set; }
    [Required] public string CustomerName { get; set; } = "";
    [Required] public string Mobile { get; set; } = "";

    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }

    [StringLength(500)]
    public string? ReviewText { get; set; }
}
